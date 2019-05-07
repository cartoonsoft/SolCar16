using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Interfaces.Repositories;

namespace Domain.Car16.Interfaces.Repositories
{
    public interface IRepositoriesFactoryCar16: IRepositoriesFactoryBase
    {
        IRepositoryPREIMO RepositoryPREIMO
        {
            get;
        }
    }
}
