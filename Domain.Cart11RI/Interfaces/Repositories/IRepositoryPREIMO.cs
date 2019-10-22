using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cart11RI.Entities;
using Domain.Core.Interfaces.Repositories;

namespace Domain.Cart11RI.Interfaces.Repositories
{
    public interface IRepositoryPREIMO : IRepositoryBaseRead<PREIMO>
    {
        PREIMO BuscaDadosImovel(long? IdPrenotacao = null, string NumMatricula = null);
    }

}
