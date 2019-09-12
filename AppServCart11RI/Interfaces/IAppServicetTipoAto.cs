using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServ.Core.Interfaces;
using Domain.CartNew.Entities;
using Domain.CartNew.Enumerations;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;

namespace AppServices.Cartorio.Interfaces
{
    public interface IAppServicetTipoAto : IAppServiceCartorio<DtoTipoAto, TipoAto>
    {
        IEnumerable<DtoTipoAtoList> ListaTipoAtos(long? idTipoAtoPai);
    }
}
