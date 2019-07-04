using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Cartorio.Entities.CartorioNew;

namespace DomainServices.Interfaces
{
    public interface IPaisDomainService : IDomainServiceCartorioNew<Pais>
    { 
        IEnumerable<Pais> BuscarPorNome(string nome);

    }
}
