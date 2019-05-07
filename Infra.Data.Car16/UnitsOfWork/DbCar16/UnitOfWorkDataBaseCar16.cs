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
using Infra.Data.Car16.Repositories.DbCar16;
using Infra.Data.Car16.UnitsOfWork.Base;

namespace Infra.Data.Car16.UnitsOfWork.DbCar16
{
    public class UnitOfWorkDataBaseCar16: UnitOfWorkCar16, IUnitOfWorkDataBaseCar16
    {
        private readonly IRepositoriesFactoryCar16 _repositoriesCar16;

        public UnitOfWorkDataBaseCar16(BaseDados baseDados, ContextMainCar16 context = null, InfraDataEventLogging log = null): base(baseDados, context, log)
        {
            //
            _repositoriesCar16 = new RepositoriesFactoryCar16(ContextMainCar16);
            base.Repositories = _repositoriesCar16;

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
                    if (this._repositoriesCar16 != null)
                    {
                        _repositoriesCar16.Dispose();
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


        public new IRepositoriesFactoryCar16 Repositories
        {
            get { return _repositoriesCar16; }

        }


    }
}
