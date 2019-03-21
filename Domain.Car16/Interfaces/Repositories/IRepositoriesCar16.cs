using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Interfaces.Repositories;

namespace Domain.Car16.Interfaces.Repositories
{
    public interface IRepositoriesCar16: IRepositoriesBase
    {
        IRepositoryPais RepositoryPais {
            get;
        }
        IRepositoryMunicipio RepositoryMunicipio
        {
            get;
        }
        IRepositoryUf RepositoryUf {
            get;
        }

    }

}
