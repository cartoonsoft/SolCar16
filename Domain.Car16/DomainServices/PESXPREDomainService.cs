using Domain.Car16.DomainServices.Base;
using Domain.Car16.Entities.Car16;
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
    public class PESXPREDomainService : DomainServiceCar16<PESXPRE>, IPESXPREDomainService
    {
        private readonly IRepositoryPESXPRE _repo;
        public PESXPREDomainService(IUnitOfWorkCar16 unitOfWorkCar16) : base(unitOfWorkCar16)
        {
            _repo = unitOfWorkCar16.Repositories.RepositoryPESXPRE;
        }

        public PESXPRE GetPESXPRE(long numeroPrenotacao)
        {
            return _repo.GetWhere(p => p.SEQPRE == numeroPrenotacao).FirstOrDefault();
        }
    }
}
