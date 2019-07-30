using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cart.Interfaces.UnitOfWork;
using Domain.Core.Interfaces.DomainServices;

namespace DomainServ.Cart.Interfaces.Base
{
    public interface IDomainServiceCartorio<TEntity>: IDomainServiceBase<TEntity> where TEntity: class
    {
        IUnitOfWorkDataBaseCartorio UfwCart
        {
            get;
        }

    }
}
