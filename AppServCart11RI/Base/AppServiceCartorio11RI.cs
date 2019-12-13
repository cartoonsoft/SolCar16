using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServ.Core.AppServices;
using Domain.CartNew.Interfaces.UnitOfWork;
using DomainServ.CartNew.Interfaces.Factory;

namespace AppServCart11RI.Base
{
    public class AppServiceCartorio11RI<TDtoEntityModel, TEntity> : AppServiceCartorio<TDtoEntityModel, TEntity> where TDtoEntityModel : class where TEntity : class
    {
        public AppServiceCartorio11RI(IUnitOfWorkDataBaseCartNew UfwCartNew, long IdCtaAcessoSist, IDomainServicesFactoryCartNew dsFactoryCartNew = null) : base(UfwCartNew, IdCtaAcessoSist, dsFactoryCartNew)
        {

        }
    }
}
