using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainServices.Base;
using DomainServices.Interfaces;
using Domain.Cartorio.Entities.CartorioNew;
using Domain.Cartorio.Entities.Diversas;
using Domain.Cartorio.Interfaces.Repositories;
using Domain.Cartorio.Interfaces.UnitOfWork;

namespace DomainServices.Services
{
    public class ArquivoModeloDocxDs: DomainServiceCartorioNew<ArquivoModeloDocx>, IArquivoModeloDocxDs
    {
        private readonly IRepositoryArquivoModeloDocx _repositoryArquivoModeloDocx;
        private readonly IRepositoryLogArquivoModeloDocx _repositoryLogArquivoModeloDocx;


        public ArquivoModeloDocxDs(IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartorioNew UfwCartNew) : base(UfwCart, UfwCartNew)
        {
            //IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartorioNew UfwCartNew
            _repositoryArquivoModeloDocx = this.UfwCartNew.Repositories.RepositoryArquivoModeloDocx;
            _repositoryLogArquivoModeloDocx = this.UfwCartNew.Repositories.RepositoryLogArquivoModeloDocx;
        }

        public IEnumerable<ArquivoModeloDocxList> ListarArquivoModeloDocx(long? IdTipoAto = null)
        {
            return _repositoryArquivoModeloDocx.ListarArquivoModeloDocx(IdTipoAto);
        }

        public IEnumerable<ArquivoModeloSimplificadoDocxList> ListarArquivoModeloSimplificadoDocx(long? IdTipoAto = null)
        {
            return _repositoryArquivoModeloDocx.ListarArquivoModeloSimplificadoDocx(IdTipoAto);
        }

        public long? SalvarModelo(ArquivoModeloDocx arquivoModeloDocx, LogArquivoModeloDocx logArquivoModeloDocx, string IdUsuario)
        {
            long? NovoId = null;

            UfwCartNew.BeginTransaction();

            NovoId = _repositoryArquivoModeloDocx.GetNextValFromOracleSequence("SQ_MODELO_DOC");
            arquivoModeloDocx.Id = NovoId;
            arquivoModeloDocx.CaminhoEArquivo = arquivoModeloDocx.CaminhoEArquivo + NovoId.ToString() + ".docx";

            _repositoryArquivoModeloDocx.Add(arquivoModeloDocx);
            UfwCartNew.SaveChanges();

            logArquivoModeloDocx.Id = _repositoryArquivoModeloDocx.GetNextValFromOracleSequence("SQ_LOG_ARQ_MOD_DOCX");
            logArquivoModeloDocx.IdArquivoModeloDocx = NovoId??0;

            _repositoryLogArquivoModeloDocx.Add(logArquivoModeloDocx);
            UfwCartNew.SaveChanges();

            UfwCartNew.CommitTransaction();

            return NovoId;
        }

        public long? EditarModelo(LogArquivoModeloDocx logArquivoModeloDocx)
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

            ArquivoModeloDocx arquivoModeloDocx = _repositoryArquivoModeloDocx.GetById(Id);

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
