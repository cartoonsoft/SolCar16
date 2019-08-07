using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServ.Core.Interfaces;
using Domain.CartNew.Interfaces.UnitOfWork;
using Domain.Core.Interfaces.DomainServices;
using Domain.Core.Interfaces.UnitOfWork;
using DomainServ.CartNew.Factory;

namespace AppServ.Core.AppServices
{
    public class AppServiceCartorio<TDtoEntityModel, TEntity> : AppServiceBase<TDtoEntityModel, TEntity>, IAppServiceCartorio<TDtoEntityModel, TEntity> where TDtoEntityModel : class where TEntity : class
    {
        private readonly IUnitOfWorkDataBaseCartNew _ufwCartNew;
        private readonly IDomainServicesFactoryBase _dsFactoryBase;

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public AppServiceCartorio(IUnitOfWorkDataBaseCartNew UfwCartNew, IDomainServicesFactoryBase dsFactoryBase = null) : base(UfwCartNew, dsFactoryBase)
        {
            this._ufwCartNew = UfwCartNew;
            this._dsFactoryBase = dsFactoryBase;

            if (_dsFactoryBase == null)
            {
                _dsFactoryBase = new DomainServicesFactoryCartNew(_ufwCartNew);
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
                    //if (_dsFactoryBase != null)
                    //{
                    //    _dsFactoryBase.Dispose();
                    //}
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

        public virtual IUnitOfWorkDataBaseCartNew UfwCartNew
        {
            get { return this._ufwCartNew; }
        }

    }
}
