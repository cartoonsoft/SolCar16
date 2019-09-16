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
using Dto.CartNew.Entities.Cart_11RI.Diversos;

namespace DomainServ.CartNew.Services
{
    public class ModeloDocxDs: DomainServiceCartNew<ModeloDocx>, IModeloDocxDs
    {
        private readonly IRepositoryModeloDocx _repositoryModeloDocx;
        private readonly IRepositoryLogModeloDocx _repositoryLogModeloDocx;

        public ModeloDocxDs(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            //
            _repositoryModeloDocx = this.UfwCartNew.Repositories.RepositoryModeloDocx;
            _repositoryLogModeloDocx = this.UfwCartNew.Repositories.RepositoryLogModeloDocx;
        }

        public IEnumerable<DtoModeloDocxList> ListarModelosDocx(long? IdTipoAto = null)
        {
            IEnumerable<DtoModeloDocxList> listaModeloDocxLists = new List<DtoModeloDocxList>();

            listaModeloDocxLists =
                from M in _repositoryModeloDocx.Get().Where(m => (IdTipoAto == null) || (m.IdTipoAto == IdTipoAto))
                join TA in this.UfwCartNew.Repositories.GenericRepository<TipoAto>().Get() on M.IdTipoAto equals TA.Id into _a
                from TA in _a.DefaultIfEmpty()
                orderby (M.DescricaoModelo)
                select new DtoModeloDocxList
                {
                    Id = M.Id,
                    IdCtaAcessoSist = M.IdCtaAcessoSist,
                    IdTipoAto = M.IdTipoAto,
                    IdUsuarioCadastro = M.IdUsuarioCadastro,
                    IdUsuarioAlteracao = M.IdUsuarioAlteracao,
                    DataCadastro = M.DataCadastro,
                    DataAlteracao = M.DataAlteracao,
                    NomeModelo = M.DescricaoModelo,
                    CaminhoEArquivo = M.CaminhoEArquivo,
                    DescricaoTipoAto = TA.Descricao,
                    Ativo = M.Ativo
                };

            return listaModeloDocxLists;
        }

        /*
        public IEnumerable<ArquivoModeloSimplificadoDocxList> ListarArquivoModeloSimplificadoDocx(long? IdTipoAto = null)
        {
            return _repositoryArquivoModeloDocx.ListarArquivoModeloSimplificadoDocx(IdTipoAto);
        }
        */

        public long? NovoModelo(ModeloDocx modeloDocx, LogModeloDocx logModeloDocx, string IdUsuario)
        {
            long? NovoId = null;

            UfwCartNew.BeginTransaction();

            NovoId = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetNextValFromOracleSequence("SQ_MODELO_DOC");
            modeloDocx.Id = NovoId;
            modeloDocx.CaminhoEArquivo = modeloDocx.CaminhoEArquivo + "modelo_" + NovoId.ToString() + ".docx";

            this.UfwCartNew.Repositories.RepositoryModeloDocx.Add(modeloDocx);
            UfwCartNew.SaveChanges();

            logModeloDocx.Id = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetNextValFromOracleSequence("SQ_LOG_ARQ_MOD_DOCX");
            logModeloDocx.IdModeloDocx = NovoId??0;

            _repositoryLogModeloDocx.Add(logModeloDocx);
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
