using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.DomainServices.Base;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.Entities.Diversas;
using Domain.Car16.Interfaces.DomainServices;
using Domain.Car16.Interfaces.Repositories;
using Domain.Car16.Interfaces.UnitOfWork;

namespace Domain.Car16.DomainServices
{
    public class ArquivoModeloDocxDomainService : DomainServiceCar16<ArquivoModeloDocx>, IArquivoModeloDocxDomainService
    {
        private readonly IRepositoryArquivoModeloDocx _repositoryArquivoModeloDocx;
        private readonly IRepositoryLogArquivoModeloDocx _repositoryLogArquivoModeloDocx;

        public ArquivoModeloDocxDomainService(IUnitOfWorkCar16 unitOfWorkCar16): base(unitOfWorkCar16)
        {
            _repositoryArquivoModeloDocx = this.UnitOfWorkCar16.Repositories.RepositoryArquivoModeloDocx;
            _repositoryLogArquivoModeloDocx = this.UnitOfWorkCar16.Repositories.RepositoryLogArquivoModeloDocx;
        }

        public IEnumerable<ArquivoModeloDocxList> ListarArquivoModeloDocx(long? IdTipoAto = null)
        {
            return _repositoryArquivoModeloDocx.ListarArquivoModeloDocx(IdTipoAto);
        }

        public IEnumerable<ArquivoModeloSimplificadoDocxList> ListarArquivoModeloSimplificadoDocx(long? IdTipoAto = null)
        {
            return _repositoryArquivoModeloDocx.ListarArquivoModeloSimplificadoDocx(IdTipoAto);
        }

        public void SalvarModelo(ArquivoModeloDocx arquivoModeloDocx, LogArquivoModeloDocx logArquivoModeloDocx, string IdUsuario)
        {
            long IdTmp = _repositoryArquivoModeloDocx.GetNextValFromOracleSequence("SQ_MODELO_DOC");
            arquivoModeloDocx.Id = IdTmp;
            logArquivoModeloDocx.IdArquivoModeloDocx = arquivoModeloDocx.Id??IdTmp;

            _repositoryArquivoModeloDocx.Add(arquivoModeloDocx);
            _repositoryLogArquivoModeloDocx.Add(logArquivoModeloDocx);

        }
    }
}
