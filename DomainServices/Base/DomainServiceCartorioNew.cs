using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cartorio.Interfaces.UnitOfWork;
using Domain.Core.DomainServices;
using Domain.Core.Interfaces.DomainServices;
using DomainServices.Interfaces.Base;

namespace DomainServices.Base
{
    public class DomainServiceCartorioNew<TEntity> : DomainServiceBase<TEntity>, IDomainServiceCartorioNew<TEntity>  where TEntity : class
    {
        private readonly IUnitOfWorkDataBaseCartorioNew _ufwCartNew;
        private readonly IUnitOfWorkDataBaseCartorio _ufwCart;

        public DomainServiceCartorioNew(IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartorioNew UfwCartNew) : base(UfwCartNew)
        {
            _ufwCart = UfwCart;
            _ufwCartNew = UfwCartNew;
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
        
        public IUnitOfWorkDataBaseCartorioNew UfwCartNew
        {
            get { return _ufwCartNew; }
        }

    }
}
