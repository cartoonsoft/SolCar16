using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AppServ.Core.Interfaces;
using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.UnitOfWork;
using Domain.Core.Entities.Base;
using Domain.Core.Interfaces.Repositories;
using Dto.CartNew.Entities.TodosCart;

namespace AppServ.Core.AppServices
{
    public class AppServicePais : AppServiceCartorio<DtoPais, Pais>, IAppServicePais
    {
        public AppServicePais(IUnitOfWorkDataBaseCartNew UfwCartNew, long IdCtaAcessoSist) : base(UfwCartNew, IdCtaAcessoSist)
        {
            //
        }

        IEnumerable<DtoPais> IAppServicePais.BuscarPorNome(string nome)
        {
            throw new NotImplementedException();
        }
    }
}
