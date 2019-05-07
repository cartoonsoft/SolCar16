using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Interfaces.DomainServices;
using Domain.Car16.Interfaces.UnitOfWork;
using Domain.Core.DomainServices;
using Domain.Core.Entities.Base;

namespace Domain.Car16.DomainServices.Base
{
    public class DomainServiceCar16New<TEntity> : DomainServiceBase<TEntity>, IDomainServiceCar16New<TEntity> where TEntity: class
    {
        private readonly IUnitOfWorkDataBaseCar16New _unitOfWorkCar16New;

        public DomainServiceCar16New(IUnitOfWorkDataBaseCar16New unitOfWorkCar16) : base(unitOfWorkCar16)
        {
            _unitOfWorkCar16New = unitOfWorkCar16;
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

        public IUnitOfWorkDataBaseCar16New UnitOfWorkCar16New
        {
            get { return _unitOfWorkCar16New; }
        }

    }
}
