using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Interfaces.UnitOfWork;
using Domain.Core.Entities.Base;
using Domain.Core.Interfaces.DomainServices.Base;

namespace Domain.Car16.Interfaces.DomainServices.Base
{
    public interface IDomainServiceCar16<TEntity>: IDomainServiceBase<TEntity> where TEntity: EntityBase
    {

        IUnitOfWorkCar16 UnitOfWorkCar16
        {
            get;
        }

    }
}
