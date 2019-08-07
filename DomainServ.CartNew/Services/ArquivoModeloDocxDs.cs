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
    public class ArquivoModeloDocxDs: DomainServiceCartNew<ModeloDocx>, IArquivoModeloDocxDs
    {
        private readonly IRepositoryModeloDocx _repositoryArquivoModeloDocx;
        private readonly IRepositoryLogModeloDocx _repositoryLogArquivoModeloDocx;

        public ArquivoModeloDocxDs(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            //IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartNew UfwCartNew
            _repositoryArquivoModeloDocx = this.UfwCartNew.Repositories.RepositoryModeloDocx;
            _repositoryLogArquivoModeloDocx = this.UfwCartNew.Repositories.RepositoryLogModeloDocx;
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

            NovoId = _repositoryArquivoModeloDocx.GetNextValFromOracleSequence("SQ_MODELO_DOC");
            arquivoModeloDocx.Id = NovoId;
            arquivoModeloDocx.CaminhoEArquivo = arquivoModeloDocx.CaminhoEArquivo + NovoId.ToString() + ".docx";

            _repositoryArquivoModeloDocx.Add(arquivoModeloDocx);
            UfwCartNew.SaveChanges();

            logArquivoModeloDocx.Id = _repositoryArquivoModeloDocx.GetNextValFromOracleSequence("SQ_LOG_ARQ_MOD_DOCX");
            logArquivoModeloDocx.IdModeloDocx = NovoId??0;

            _repositoryLogArquivoModeloDocx.Add(logArquivoModeloDocx);
            UfwCartNew.SaveChanges();

            UfwCartNew.CommitTransaction();

            return NovoId;
        }

        public long? EditarModelo(ModeloDocx arquivoModeloDocx, LogModeloDocx logArquivoModeloDocx, string IdUsuario)
        {
            long? NovoId = null;

            UfwCartNew.BeginTransaction();
            
            logArquivoModeloDocx.Id = _repositoryArquivoModeloDocx.GetNextValFromOracleSequence("SQ_LOG_ARQ_MOD_DOCX");

            _repositoryLogArquivoModeloDocx.Add(logArquivoModeloDocx);
            UfwCartNew.SaveChanges();

            UfwCartNew.CommitTransaction();

            return NovoId;
        }

        public bool Desativar(long Id, string IdUsuario)
        {
            bool resultado = false;

            ModeloDocx arquivoModeloDocx = _repositoryArquivoModeloDocx.GetById(Id);

            if (arquivoModeloDocx != null)
            {
                arquivoModeloDocx.Ativo = false;
                arquivoModeloDocx.IdUsuarioAlteracao = IdUsuario;
                _repositoryArquivoModeloDocx.Update(arquivoModeloDocx);
                UfwCartNew.SaveChanges();
                resultado = true;
            }

            return resultado;
        }
    }
}
