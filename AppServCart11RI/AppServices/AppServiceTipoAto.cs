using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServ.Core.AppServices;
using AppServices.Cartorio.Interfaces;
using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.UnitOfWork;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;

namespace AppServCart11RI.AppServices
{
    public class AppServiceTipoAto: AppServiceCartorio<DtoTipoAto, TipoAto>, IAppServicetTipoAto
    {
        public AppServiceTipoAto(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            //
        }

        public IEnumerable<DtoTipoAtoList> ListaTipoAtos(long? idTipoAtoPai)
        {
            return this.DsFactoryCartNew.TipoAtoDs.ListaTipoAtos(idTipoAtoPai);
        }
    }
}
