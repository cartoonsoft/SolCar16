using Domain.Car16.Entities.Car16;
using Domain.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.Interfaces.Repositories
{
    public interface IRepositoryPREIMO : IRepositoryBaseRead<PREIMO>
    {
        PREIMO BuscaDadosImovel(long? numeroPrenotacao = null, long? numeroMatricula = null);

    }
}
