using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.CartNew.Entities;
using Domain.CartNew.Entities.Diversos;
using Domain.CartNew.Interfaces.Repositories;
using Domain.CartNew.Interfaces.UnitOfWork;
using DomainServ.CartNew.Base;
using DomainServ.CartNew.Interfaces;
using Dto.CartNew.Entities.Cart_11RI.Diversos;

namespace DomainServ.CartNew.Services
{
    public class ModeloDocxDs: DomainServiceCartNew<ModeloDoc>, IModeloDocxDs
    {
        private readonly IRepositoryModeloDoc _repositoryModeloDocx;
        private readonly IRepositoryLogModeloDoc _repositoryLogModeloDocx;

        public ModeloDocxDs(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            _repositoryModeloDocx = this.UfwCartNew.Repositories.RepositoryModeloDocx;
            _repositoryLogModeloDocx = this.UfwCartNew.Repositories.RepositoryLogModeloDocx;
        }

        public long? NovoModelo(ModeloDoc modeloDocx, LogModeloDoc logModeloDocx, string IdUsuario)
        {
            long? NovoId = null; 

            if (modeloDocx != null)
            {
                UfwCartNew.BeginTransaction();
                NovoId = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetNextValFromOracleSequence("SQ_MODELO_DOC");

                modeloDocx.Id = NovoId;
                modeloDocx.CaminhoEArquivo = modeloDocx.CaminhoEArquivo + "modelo_" + modeloDocx.Id.ToString() + ".docx";

                this.UfwCartNew.Repositories.RepositoryModeloDocx.Add(modeloDocx);
                UfwCartNew.SaveChanges();

                if (logModeloDocx != null) {
                    logModeloDocx.Id = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetNextValFromOracleSequence("SQ_LOG_ARQ_MOD_DOCX");
                    logModeloDocx.IdModeloDoc = modeloDocx.Id ?? 0;

                    _repositoryLogModeloDocx.Add(logModeloDocx);
                    UfwCartNew.SaveChanges();
                }
                UfwCartNew.CommitTransaction();
            }

            return NovoId;
        }

        public long? EditarModelo(ModeloDoc arquivoModeloDocx, LogModeloDoc logArquivoModeloDocx, string IdUsuario)
        {
            long? NovoId = null;

            if (logArquivoModeloDocx != null)
            {
                UfwCartNew.BeginTransaction();

                logArquivoModeloDocx.Id = _repositoryModeloDocx.GetNextValFromOracleSequence("SQ_LOG_ARQ_MOD_DOCX");
                _repositoryLogModeloDocx.Add(logArquivoModeloDocx);
                UfwCartNew.SaveChanges();

                UfwCartNew.CommitTransaction();
            }

            return NovoId;
        }

        public bool Desativar(long Id, string IdUsuario)
        {
            bool resultado = false;

            ModeloDoc modeloDocx = _repositoryModeloDocx.GetById(Id);

            if (modeloDocx != null)
            {
                modeloDocx.Ativo = false;
                modeloDocx.IdUsuarioAlteracao = IdUsuario;
                _repositoryModeloDocx.Update(modeloDocx);
                UfwCartNew.SaveChanges();
                resultado = true;
            }

            return resultado;
        }

        public IEnumerable<DtoModeloDocxList> GetListModelosDocx(long? IdTipoAto = null)
        {
            IEnumerable<DtoModeloDocxList> listaModeloDocxList = new List<DtoModeloDocxList>();

            List<ModeloDocxList> lista = _repositoryModeloDocx.GetListModelosDocx(IdTipoAto).ToList();
            listaModeloDocxList = Mapper.Map<List<ModeloDocxList>, List<DtoModeloDocxList>>(lista);

            return listaModeloDocxList;
        }

    }
}
