using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities;
using Domain.Car16.Interfaces.DomainServices;
using Domain.Car16.Interfaces.Repositories;
using Domain.Car16.Interfaces.UnitOfWork;
using Domain.Core.DomainServices.Base;

namespace Domain.Car16.DomainServices
{
    public class PaisDomainService : DomainServiceBase<Pais>, IPaisDomainService
    {

        private readonly IUnitOfWorkCar16 _unitOfWorkCar16;
        private readonly IRepositoryPais repositoryPais = null;

        public PaisDomainService(IUnitOfWorkCar16 unitOfWorkCar16) : base(unitOfWorkCar16)
        {
            _unitOfWorkCar16 = unitOfWorkCar16;
            repositoryPais = _unitOfWorkCar16.repositories.RepositoryPais;

        }

        public IEnumerable<Pais> BuscarPorNome(string nome)
        {
            return repositoryPais.BuscarPorNome(nome);
        }
    }
}
