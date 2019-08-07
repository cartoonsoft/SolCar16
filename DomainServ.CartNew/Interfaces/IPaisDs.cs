using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.CartNew.Entities;
using DomainServ.CartNew.Interfaces.Base;

namespace DomainServ.CartNew.Interfaces
{
    public interface IPaisDs : IDomainServiceCartNew<Pais>
    { 
        IEnumerable<Pais> BuscarPorNome(string nome);
    }
}
