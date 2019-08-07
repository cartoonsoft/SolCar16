using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.CartNew.Entities;
using Dto.CartNew.Entities.TodosCart;

namespace AppServ.Core.Interfaces
{
    public interface IAppServicePais : IAppServiceCartorio<DtoPais, Pais>
    {
        IEnumerable<DtoPais> BuscarPorNome(string nome);
    }
}
