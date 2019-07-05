using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Cartorio.Entities.CartorioNew;
using DomainServices.Interfaces.Base;

namespace DomainServices.Interfaces
{
    public interface IPaisDs : IDomainServiceCartorioNew<Pais>
    { 
        IEnumerable<Pais> BuscarPorNome(string nome);

    }
}
