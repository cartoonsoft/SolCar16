using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Interfaces.DomainServices.Base;
using Domain.Car16.Interfaces.UnitOfWork;
using Domain.Core.DomainServices.Base;
using Domain.Core.Entities.Base;

namespace Domain.Car16.DomainServices.Base
{
    public class DomainServiceCar16<TEntity> : DomainServiceBase<TEntity>, IDomainServiceCar16<TEntity> where TEntity: EntityBase
    {
        private readonly IUnitOfWorkCar16 _unitOfWorkCar16;

        public DomainServiceCar16(IUnitOfWorkCar16 unitOfWorkCar16) : base(unitOfWorkCar16)
        {
            _unitOfWorkCar16 = unitOfWorkCar16;
        }

        public IUnitOfWorkCar16 UnitOfWorkCar16
        {
            get { return _unitOfWorkCar16; }

        }

    }
}
