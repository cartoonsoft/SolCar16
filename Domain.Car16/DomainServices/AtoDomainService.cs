using Domain.Car16.DomainServices.Base;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.Interfaces.DomainServices;
using Domain.Car16.Interfaces.Repositories;
using Domain.Car16.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.DomainServices
{
    public class AtoDomainService : DomainServiceCar16<Ato>, IAtoDomainService
    {
        private readonly IRepositoryAto _repo;

        public AtoDomainService(IUnitOfWorkCar16 unitOfWorkCar16) : base(unitOfWorkCar16)
        {
            _repo = unitOfWorkCar16.Repositories.RepositoryAto;
        }

        public bool CadastrarAto(Ato ato)
        {
            try
            {
                _repo.Add(ato);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            
        }
    }
}
