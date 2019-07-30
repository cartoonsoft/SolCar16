using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cart.Interfaces.UnitOfWork;
using Domain.Core.DomainServices;
using DomainServ.Cart.Interfaces.Base;

namespace DomainServ.Cart.Base
{
    public class DomainServiceCartorio<TEntity> : DomainServiceBase<TEntity>, IDomainServiceCartorio<TEntity>  where TEntity : class
    {
        private readonly IUnitOfWorkDataBaseCartorio _ufwCart;

        public DomainServiceCartorio(IUnitOfWorkDataBaseCartorio UfwCart) : base(UfwCart)
        {
            _ufwCart = UfwCart;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.

                disposedValue = true;
            }

            base.Dispose(disposing);
        }

        public new void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        public IUnitOfWorkDataBaseCartorio UfwCart
        {
            get { return _ufwCart; }
        }
        
        public IUnitOfWorkDataBaseCartorio UfwCartNew
        {
            get { return _ufwCart; }
        }

    }
}
