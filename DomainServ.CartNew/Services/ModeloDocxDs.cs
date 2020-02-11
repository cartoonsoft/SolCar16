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
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;

namespace DomainServ.CartNew.Services
{
    public class ModeloDocxDs: DomainServiceCartNew<ModeloDoc>, IModeloDocxDs
    {
        public ModeloDocxDs(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            //
        }

        public long? NovoModelo(DtoModeloDoc dtoModeloDoc)
        {
            long? NovoId = null; 

            if (dtoModeloDoc != null)
            {
                UfwCartNew.BeginTransaction();
                NovoId = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetNextValFromOracleSequence("SQ_MODELO_DOC");

                // Criando objeto do arquivo 
                ModeloDoc modeloDoc = new ModeloDoc
                {
                    Id = NovoId,
                    IdCtaAcessoSist = dtoModeloDoc.IdCtaAcessoSist,
                    IdTipoAto = dtoModeloDoc.IdTipoAto,
                    IdUsuarioCadastro = dtoModeloDoc.IdUsuarioCadastro,
                    DataCadastro = dtoModeloDoc.DataCadastro,
                    Descricao = dtoModeloDoc.Descricao,
                    Texto = dtoModeloDoc.Texto,
                    Orientacao = dtoModeloDoc.Orientacao,
                    Ativo = dtoModeloDoc.Ativo
                };

                // Registro de Log                
                LogModeloDoc logModeloDocx = new LogModeloDoc()
                {
                    Id = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetNextValFromOracleSequence("SQ_LOG_ARQ_MOD_DOCX"),
                    IdModeloDoc = modeloDoc.Id ?? 0,
                    IdUsuario = dtoModeloDoc.IdUsuarioCadastro,
                    DataHora = DateTime.Now,
                    UsuarioSistOperacional = dtoModeloDoc.UsuarioSistOperacional,
                    IP = dtoModeloDoc.IpLocal
                };

                this.UfwCartNew.Repositories.RepositoryModeloDocx.Add(modeloDoc);
                this.UfwCartNew.Repositories.RepositoryLogModeloDocx.Add(logModeloDocx);

                UfwCartNew.SaveChanges();
                UfwCartNew.CommitTransaction();
            }

            return NovoId;
        }

        public void EditarModelo(DtoModeloDoc dtoModeloDoc)
        {
            if (dtoModeloDoc != null)
            {
                ModeloDoc modeloDoc = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetById(dtoModeloDoc.Id);

                if (modeloDoc != null)
                {
                    UfwCartNew.BeginTransaction();

                    modeloDoc.IdTipoAto = dtoModeloDoc.IdTipoAto;
                    modeloDoc.IdUsuarioAlteracao = dtoModeloDoc.IdUsuarioAlteracao;
                    modeloDoc.DataAlteracao = dtoModeloDoc.DataAlteracao;
                    modeloDoc.Descricao = dtoModeloDoc.Descricao;
                    modeloDoc.Texto = dtoModeloDoc.Texto;
                    modeloDoc.Orientacao = dtoModeloDoc.Orientacao;
                    modeloDoc.Ativo = dtoModeloDoc.Ativo;

                    // Registro de Log                
                    LogModeloDoc logModeloDocx = new LogModeloDoc()
                    {
                        Id = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetNextValFromOracleSequence("SQ_LOG_ARQ_MOD_DOCX"),
                        IdModeloDoc = dtoModeloDoc.Id ?? 0,
                        IdUsuario = dtoModeloDoc.IdUsuarioAlteracao,
                        DataHora = dtoModeloDoc.DataAlteracao ?? DateTime.Now,
                        UsuarioSistOperacional = dtoModeloDoc.UsuarioSistOperacional,
                        IP = dtoModeloDoc.IpLocal
                    };

                    this.UfwCartNew.Repositories.RepositoryModeloDocx.Update(modeloDoc);
                    this.UfwCartNew.Repositories.RepositoryLogModeloDocx.Add(logModeloDocx);

                    UfwCartNew.SaveChanges();
                    UfwCartNew.CommitTransaction();
                }
            }
        }

        public bool Desativar(long Id, string IdUsuario)
        {
            bool resultado = false;

            ModeloDoc modeloDocx = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetById(Id);

            if (modeloDocx != null)
            {
                modeloDocx.Ativo = false;
                modeloDocx.IdUsuarioAlteracao = IdUsuario;
                this.UfwCartNew.Repositories.RepositoryModeloDocx.Update(modeloDocx);
                UfwCartNew.SaveChanges();
                resultado = true;
            }

            return resultado;
        }

        public IEnumerable<DtoModeloDocxList> GetListModelosDocx(long? IdTipoAto = null)
        {
            IEnumerable<DtoModeloDocxList> listaModeloDocxList = new List<DtoModeloDocxList>();

            List<ModeloDocxList> lista = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetListModelosDocx(IdTipoAto).ToList();
            listaModeloDocxList = Mapper.Map<List<ModeloDocxList>, List<DtoModeloDocxList>>(lista);

            return listaModeloDocxList;
        }

    }
}
