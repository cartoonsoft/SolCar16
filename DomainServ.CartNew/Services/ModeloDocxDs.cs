using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.Repositories;
using Domain.CartNew.Interfaces.UnitOfWork;
using DomainServ.CartNew.Base;
using DomainServ.CartNew.Interfaces;

namespace DomainServ.CartNew.Services
{
    public class ModeloDocxDs: DomainServiceCartNew<ModeloDocx>, IModeloDocxDs
    {
        private readonly IRepositoryModeloDocx _repositoryModeloDocx;
        private readonly IRepositoryLogModeloDocx _repositoryLogModeloDocx;

        public ModeloDocxDs(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            //IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartNew UfwCartNew
            _repositoryModeloDocx = this.UfwCartNew.Repositories.RepositoryModeloDocx;
            _repositoryLogModeloDocx = this.UfwCartNew.Repositories.RepositoryLogModeloDocx;
        }

        /*
        public IEnumerable<ArquivoModeloDocxList> ListarArquivoModeloDocx(long? IdTipoAto = null)
        {
            return _repositoryArquivoModeloDocx.ListarArquivoModeloDocx(IdTipoAto);
        }

        public IEnumerable<ArquivoModeloSimplificadoDocxList> ListarArquivoModeloSimplificadoDocx(long? IdTipoAto = null)
        {
            return _repositoryArquivoModeloDocx.ListarArquivoModeloSimplificadoDocx(IdTipoAto);
        }
        */

        public long? NovoModelo(ModeloDocx arquivoModeloDocx, LogModeloDocx logArquivoModeloDocx, string IdUsuario)
        {
            long? NovoId = null;

            UfwCartNew.BeginTransaction();

            NovoId = _repositoryModeloDocx.GetNextValFromOracleSequence("SQ_MODELO_DOC");
            arquivoModeloDocx.Id = NovoId;
            arquivoModeloDocx.CaminhoEArquivo = arquivoModeloDocx.CaminhoEArquivo + NovoId.ToString() + ".docx";

            _repositoryModeloDocx.Add(arquivoModeloDocx);
            UfwCartNew.SaveChanges();

            logArquivoModeloDocx.Id = _repositoryModeloDocx.GetNextValFromOracleSequence("SQ_LOG_ARQ_MOD_DOCX");
            logArquivoModeloDocx.IdModeloDocx = NovoId??0;

            _repositoryLogModeloDocx.Add(logArquivoModeloDocx);
            UfwCartNew.SaveChanges();

            UfwCartNew.CommitTransaction();

            return NovoId;
        }

        public long? EditarModelo(ModeloDocx arquivoModeloDocx, LogModeloDocx logArquivoModeloDocx, string IdUsuario)
        {
            long? NovoId = null;

            UfwCartNew.BeginTransaction();
            
            logArquivoModeloDocx.Id = _repositoryModeloDocx.GetNextValFromOracleSequence("SQ_LOG_ARQ_MOD_DOCX");

            _repositoryLogModeloDocx.Add(logArquivoModeloDocx);
            UfwCartNew.SaveChanges();

            UfwCartNew.CommitTransaction();

            return NovoId;
        }

        public bool Desativar(long Id, string IdUsuario)
        {
            bool resultado = false;

            ModeloDocx arquivoModeloDocx = _repositoryModeloDocx.GetById(Id);

            if (arquivoModeloDocx != null)
            {
                arquivoModeloDocx.Ativo = false;
                arquivoModeloDocx.IdUsuarioAlteracao = IdUsuario;
                _repositoryModeloDocx.Update(arquivoModeloDocx);
                UfwCartNew.SaveChanges();
                resultado = true;
            }

            return resultado;
        }
    }
}
