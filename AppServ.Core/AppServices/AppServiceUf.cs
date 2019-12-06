using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.UnitOfWork;
using Dto.CartNew.Entities.TodosCart;
using AppServ.Core.Interfaces;

namespace AppServ.Core.AppServices
{
    public class AppServiceUf : AppServiceCartorio<DtoUf, Uf>, IAppServiceUf
    {
        public AppServiceUf(IUnitOfWorkDataBaseCartNew UfwCartNew, long IdCtaAcessoSist) : base(UfwCartNew, IdCtaAcessoSist)
        {
            //
        }
    }

}
