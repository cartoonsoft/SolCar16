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
    public class PaisDomainService : DomainServiceCar16<Pais>, IPaisDomainService
    {
        private readonly IRepositoryPais _repositoryPais = null;

        public PaisDomainService(IUnitOfWorkCar16 unitOfWorkCar16) : base(unitOfWorkCar16)
        {
            _repositoryPais = this.UnitOfWorkCar16.Repositories.RepositoryPais;
            
        }

        public IEnumerable<Pais> BuscarPorNome(string nome)
        {
            return _repositoryPais.BuscarPorNome(nome);
        }
    }
}
