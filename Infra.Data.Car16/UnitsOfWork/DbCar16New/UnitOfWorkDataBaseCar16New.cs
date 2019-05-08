using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.enums;
using Domain.Car16.Interfaces.Repositories;
using Domain.Car16.Interfaces.UnitOfWork;
using Infra.Data.Car16.Context;
using Infra.Data.Car16.Repositories.Base;
using Infra.Data.Car16.Repositories.DbCar16New;
using Infra.Data.Car16.UnitsOfWork.Base;

namespace Infra.Data.Car16.UnitsOfWork.DbCar16New
{
    public class UnitOfWorkDataBaseCar16New: UnitOfWorkCar16, IUnitOfWorkDataBaseCar16New
    {
        private readonly ContextMainCar16New _context;
        private readonly IRepositoriesFactoryCar16New _repositoriesCar16New;

        public UnitOfWorkDataBaseCar16New(BaseDados baseDados, ContextMainCar16New context = null, InfraDataEventLogging log = null): base(baseDados, context, log)
        {
            _context = context;

            if (_context == null)
            {
                _context = new ContextMainCar16New(GetContextName(baseDados));
                base.Context = _context;
            }

            _repositoriesCar16New = new RepositoriesFactoryCar16New(_context);
            base.Repositories = _repositoriesCar16New;
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
                    if (this._repositoriesCar16New != null)
                    {
                        _repositoriesCar16New.Dispose();
                    }
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

        public new IRepositoriesFactoryCar16New Repositories
        {
            get { return _repositoriesCar16New; }

        }

        ContextMainCar16New ContextCar16New
        {
            get { return _context; }
        }

    }
}
