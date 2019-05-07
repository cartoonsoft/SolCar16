using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.DomainServices.Base;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.Interfaces.DomainServices;
using Domain.Car16.Interfaces.Repositories;
using Domain.Car16.Interfaces.UnitOfWork;

namespace Domain.Car16.DomainServices
{
    public class UfDomainService : DomainServiceCar16New<Uf> , IUfDomainService
    {
        private readonly IRepositoryUf _repositoryUf;

        public UfDomainService(IUnitOfWorkDataBaseCar16New unitOfWorkCar16) : base(unitOfWorkCar16)
        {
            _repositoryUf = this.UnitOfWorkCar16New.Repositories.RepositoryUf;
        }

    }
}
