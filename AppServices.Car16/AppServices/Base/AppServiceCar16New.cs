using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Car16.Interfaces.Base;
using Domain.Car16.DomainServices.Base;
using Domain.Car16.Interfaces.DomainServices.Base;
using Domain.Car16.Interfaces.UnitOfWork;
using Domain.Core.Entities.Base;
using Dto.Car16.Entities.Base;

namespace AppServices.Car16.AppServices.Base
{
    public class AppServiceCar16New<TDtoEntityModel, TEntity>: AppServiceBase<TDtoEntityModel, TEntity>, IAppServiceCar16<TDtoEntityModel, TEntity> where TDtoEntityModel : class where TEntity : class
    {
        private readonly IUnitOfWorkDataBaseCar16New _unitOfWorkCar16New;
        private readonly IDomainServicesFactoryCar16New _domainServicesFactoryCar16New;

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public AppServiceCar16New(IUnitOfWorkDataBaseCar16New unitOfWorkCar16New) : base(unitOfWorkCar16New)
        {
            this._unitOfWorkCar16New = unitOfWorkCar16New;
            _domainServicesFactoryCar16New = new DomainServicesFactoryCar16New(this._unitOfWorkCar16New);
            base.DomainServices = _domainServicesFactoryCar16New;
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
                    if (_domainServicesFactoryCar16New != null)
                    {
                        _domainServicesFactoryCar16New.Dispose();
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

        public IUnitOfWorkDataBaseCar16New UnitOfWorkCar16New
        {
            get { return this._unitOfWorkCar16New; }
        }

        public new IDomainServicesFactoryCar16New DomainServices
        {
            get { return _domainServicesFactoryCar16New; }
        }

    }
}
