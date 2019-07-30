using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Cartorio.Interfaces.Base;
using Domain.Cart.Interfaces.UnitOfWork;
using DomainServ.Cart.Interfaces.Factory;
using Domain.CartNew.Interfaces.UnitOfWork;
using DomainServ.CartNew.Interfaces.Factory;
using DomainServ.CartNew.Factory;

namespace AppServices.Cartorio.AppServices.Base
{
    public class AppServiceCartorio<TDtoEntityModel, TEntity> : AppServiceBase<TDtoEntityModel, TEntity>, IAppServiceCartorio<TDtoEntityModel, TEntity> where TDtoEntityModel : class where TEntity : class
    {
        private readonly IUnitOfWorkDataBaseCartorio _ufwCart;
        private readonly IDomainServicesFactoryCartorio _domainServicesFactoryCartorio;

        private readonly IUnitOfWorkDataBaseCartorioNew _ufwCartNew;
        private readonly IDomainServicesFactoryCartorioNew _domainServicesFactoryCartorioNew;

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public AppServiceCartorio(IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartorioNew UfwCartNew, IDomainServicesFactoryCartorio dsFactoryCartorio = null, IDomainServicesFactoryCartorioNew dsFactoryCartorioNew = null) : base(UfwCartNew, dsFactoryCartorioNew)
        {
            this._ufwCart = UfwCart;
            this._ufwCartNew = UfwCartNew;

            if (dsFactoryCartorioNew == null)
            {
                this._domainServicesFactoryCartorioNew = new DomainServicesFactoryCartorioNew(this._ufwCartNew);
                this.DsFactoryBase = _domainServicesFactoryCartorioNew;
            }
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
                    if (_domainServicesFactoryCartorioNew != null)
                    {
                        _domainServicesFactoryCartorioNew.Dispose();
                    }
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.
                disposedValue = true;
            }
            base.Dispose(disposing);
        }

        // override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AppServiceBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public new void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
        
        public virtual IDomainServicesFactoryCartorioNew DsFactoryCartNew
        {
            get { return _domainServicesFactoryCartorioNew; }
        }

        public virtual IUnitOfWorkDataBaseCartorio UfwCart
        {
            get { return this._ufwCart; }
        }
        public virtual IUnitOfWorkDataBaseCartorioNew UfwCartNew
        {
            get { return this._ufwCartNew; }
        }

    }
}
