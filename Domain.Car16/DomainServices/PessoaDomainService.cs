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
    public class PessoaDomainService : DomainServiceCar16New<Pessoa>, IPessoaDomainService
    {
        private readonly IRepositoryPessoa _repositoryPessoa;

        public PessoaDomainService(IUnitOfWorkDataBaseCar16New unitOfWorkCar16): base(unitOfWorkCar16)
        {
            //todo: ronaldo fazer

            _repositoryPessoa = null; // this.UnitOfWorkCar16.Repositories.RepositoryPessoa;

        }

    }
}
