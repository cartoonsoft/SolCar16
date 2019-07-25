using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities.Car16New;
using Domain.Core.Interfaces.Repositories;

namespace Domain.Cartorio.Interfaces.Repositories
{
    public interface IRepositoryPais: IRepositoryBaseReadWrite<Pais>
    {

        IEnumerable<Pais> BuscarPorNome(string nome);

    }
}
