using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cart.Entities;
using Domain.Core.Interfaces.Repositories;

namespace Domain.Cart.Interfaces.Repositories
{
    public interface IRepositoryPREIMO : IRepositoryBaseRead<PREIMO>
    {
        PREIMO BuscaDadosImovel(long? numeroPrenotacao = null, long? numeroMatricula = null);

    }
}
