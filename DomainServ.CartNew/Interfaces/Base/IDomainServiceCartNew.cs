using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Interfaces.UnitOfWork;
using Domain.Core.Interfaces.DomainServices;

namespace DomainServ.CartNew.Interfaces.Base
{
    public interface IDomainServiceCartNew<TEntity>: IDomainServiceBase<TEntity> where TEntity: class
    {
        IUnitOfWorkDataBaseCartNew UfwCartNew
        {
            get;
        }

    }
}
