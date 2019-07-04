using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cartorio.Interfaces.Repositories;
using Domain.Core.Interfaces.UnitOfWork;

namespace Domain.Cartorio.Interfaces.UnitOfWork
{
    public interface IUnitOfWorkDataBaseCartorio: IunitOfWorkCartoonSoft
    {
        //
        new IRepositoriesFactoryCartorio Repositories
        {
            get;
        }
    }
}
