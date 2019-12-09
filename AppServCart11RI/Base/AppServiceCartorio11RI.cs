using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServ.Core.AppServices;
using Domain.CartNew.Interfaces.UnitOfWork;

namespace AppServCart11RI.Base
{
    public class AppServiceCartorio11RI<TDtoEntityModel, TEntity> : AppServiceCartorio<TDtoEntityModel, TEntity> where TDtoEntityModel : class where TEntity : class
    {
        public AppServiceCartorio11RI(IUnitOfWorkDataBaseCartNew UfwCartNew, long IdCtaAcessoSist) : base(UfwCartNew, IdCtaAcessoSist)
        {

        }
    }
}
