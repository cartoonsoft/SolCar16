using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Interfaces.Repositories;
using Domain.Core.Interfaces.UnitOfWork;

namespace Domain.Car16.Interfaces.UnitOfWork
{
    public interface IUnitOfWorkDataBaseCar16: IUnitOfWorkCar16
    {
        //
        new IRepositoriesFactoryCar16 Repositories
        {
            get;
        }
    }
}
