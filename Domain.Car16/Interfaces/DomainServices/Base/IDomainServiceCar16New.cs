using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Interfaces.UnitOfWork;
using Domain.Core.Entities.Base;
using Domain.Core.Interfaces.DomainServices;

namespace Domain.Car16.Interfaces.DomainServices.Base
{
    public interface IDomainServiceCar16New<TEntity>: IDomainServiceBase<TEntity> where TEntity: class
    {

        IUnitOfWorkDataBaseCar16New UnitOfWorkCar16New
        {
            get;
        }

    }
}
