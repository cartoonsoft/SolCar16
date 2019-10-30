using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServCart11RI.Base;
using AppServices.Cartorio.Interfaces;
using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.UnitOfWork;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;

namespace AppServCart11RI.AppServices
{
    public class AppServiceTipoAto: AppServiceCartorio11RI<DtoTipoAto, TipoAto>, IAppServicetTipoAto
    {
        public AppServiceTipoAto(IUnitOfWorkDataBaseCartNew UfwCartNew, long IdCtaAcessoSist) : base(UfwCartNew, IdCtaAcessoSist)
        {
            //
        }

        public IEnumerable<DtoTipoAtoList> GetListTiposAto(long? idTipoAtoPai)
        {
            return this.DsFactoryCartNew.TipoAtoDs.GetListTiposAto(idTipoAtoPai);
        }
    }
}
