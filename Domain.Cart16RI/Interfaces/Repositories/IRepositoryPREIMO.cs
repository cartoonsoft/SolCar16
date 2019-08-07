using Domain.Cart16RI.Entities;
using Domain.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Cart11RI.Interfaces.Repositories
{
    public interface IRepositoryPREIMO : IRepositoryBaseRead<PREIMO>
    {
        PREIMO BuscaDadosImovel(long? numeroPrenotacao = null, long? numeroMatricula = null);

    }
}
