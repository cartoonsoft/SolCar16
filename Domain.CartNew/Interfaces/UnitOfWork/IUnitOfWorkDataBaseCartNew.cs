using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Interfaces.Repositories;
using Domain.Core.Interfaces.UnitOfWork;

namespace Domain.CartNew.Interfaces.UnitOfWork
{
    public interface IUnitOfWorkDataBaseCartNew : IUfwCart
    {
        new IRepositoriesFactoryCartNew Repositories
        {
            get;
        }
    }
}
