using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Interfaces.UnitOfWork;
using Domain.Cart.Interfaces.Repositories;

namespace Domain.Cart.Interfaces.UnitOfWork
{
    public interface IUnitOfWorkDataBaseCartorio: IUnitOfWorkCar16
    {
        //
        new IRepositoriesFactoryCartorio Repositories
        {
            get;
        }
    }
}
