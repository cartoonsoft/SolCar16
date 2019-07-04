using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cartorio.Interfaces.UnitOfWork;
using Domain.Core.Interfaces.DomainServices;

namespace DomainServices.Interfaces
{
    public interface IDomainServiceCartorioNew<TEntity>: IDomainServiceBase<TEntity> where TEntity: class
    {
        IUnitOfWorkDataBaseCartorio UfwCart
        {
            get;
        }

        IUnitOfWorkDataBaseCartorioNew UfwCartNew
        {
            get;
        }
    }
}
