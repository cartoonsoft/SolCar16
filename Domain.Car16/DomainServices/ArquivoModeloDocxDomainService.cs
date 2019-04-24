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

        public ArquivoModeloDocxDomainService(IUnitOfWorkCar16 unitOfWorkCar16): base(unitOfWorkCar16)
        {
            //todo: ronaldo fazer

            _repositoryArquivoModeloDocx = this.UnitOfWorkCar16.Repositories.RepositoryArquivoModeloDocx;

        }

        public IEnumerable<ArquivoModeloDocxList> ListarArquivoModeloDocx(long? IdTipoAto = null)
        {
            return _repositoryArquivoModeloDocx.ListarArquivoModeloDocx(IdTipoAto);
        }

    }
}
