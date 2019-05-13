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
    public class ArquivoModeloDocxDomainService : DomainServiceCar16New<ArquivoModeloDocx>, IArquivoModeloDocxDomainService
    {
        private readonly IRepositoryArquivoModeloDocx _repositoryArquivoModeloDocx;
        private readonly IRepositoryLogArquivoModeloDocx _repositoryLogArquivoModeloDocx;

        public ArquivoModeloDocxDomainService(IUnitOfWorkDataBaseCar16New unitOfWorkCar16): base(unitOfWorkCar16)
        {
            _repositoryArquivoModeloDocx = this.UnitOfWorkCar16New.Repositories.RepositoryArquivoModeloDocx;
            _repositoryLogArquivoModeloDocx = this.UnitOfWorkCar16New.Repositories.RepositoryLogArquivoModeloDocx;
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

            UnitOfWorkCar16New.BeginTransaction();

            long IdTmp = _repositoryArquivoModeloDocx.GetNextValFromOracleSequence("SQ_MODELO_DOC");
            arquivoModeloDocx.Id = IdTmp;
            logArquivoModeloDocx.IdArquivoModeloDocx = IdTmp;

            _repositoryArquivoModeloDocx.Add(arquivoModeloDocx);
            UnitOfWorkCar16New.SaveChanges();

            _repositoryLogArquivoModeloDocx.Add(logArquivoModeloDocx);
            UnitOfWorkCar16New.SaveChanges();

            UnitOfWorkCar16New.CommitTransaction();


        }
    }
}
