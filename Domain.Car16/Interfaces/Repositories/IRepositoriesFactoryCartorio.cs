using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Interfaces.Repositories;

namespace Domain.Cartorio.Interfaces.Repositories
{
    public interface IRepositoriesFactoryCartorio: IRepositoriesFactoryBase
    {
        IRepositoryPREIMO RepositoryPREIMO
        {
            get;
        }
    }
}
