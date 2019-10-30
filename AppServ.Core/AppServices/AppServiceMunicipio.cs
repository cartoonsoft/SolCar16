using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServ.Core.Interfaces;
using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.UnitOfWork;
using Dto.CartNew.Entities.TodosCart;

namespace AppServ.Core.AppServices
{
    public class AppServiceMunicipio : AppServiceCartorio<DtoMunicipio, Municipio>, IAppServiceMunicipio
    {
        public AppServiceMunicipio(IUnitOfWorkDataBaseCartNew UfwCartNew, long IdCtaAcessoSist) : base(UfwCartNew, IdCtaAcessoSist)
        {
            //
        }

    }
}
