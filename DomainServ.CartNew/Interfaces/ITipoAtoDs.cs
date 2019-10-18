using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using DomainServ.CartNew.Interfaces.Base;
using Domain.CartNew.Entities;
using Dto.CartNew.Entities.Cart_11RI.Diversos;

namespace DomainServ.CartNew.Interfaces
{
    public interface ITipoAtoDs : IDomainServiceCartNew<TipoAto>
    {
        IEnumerable<DtoTipoAtoList> GetListTiposAto(long? idTipoAtoPai);
    }
}
