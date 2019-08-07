using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Interfaces.Repositories;
using Domain.CartNew.Interfaces.UnitOfWork;
using Infra.Data.CartNew.Context;
using Infra.Data.CartNew.Repositories.DbCartNew;
using Infra.Data.Cartorio.UnitsOfWork.Base;
using Infra.Data.Core.Repositories;

namespace Infra.Data.CartNew.UnitsOfWork.DbCartNew
{
    public class UnitOfWorkDataBaseCartNew: UfwCart, IUnitOfWorkDataBaseCartNew
    {
        private readonly ContextMainCartNew _context;
        private readonly IRepositoriesFactoryCartNew _repositoriesCartNew;

        public UnitOfWorkDataBaseCartNew(string contextName, ContextMainCartNew context = null, InfraDataEventLogging log = null): base(context, log)
        {
            _context = context;

            if (_context == null)
            {
                _context = new ContextMainCartNew(contextName);
                base.Context = _context;
            }

            _repositoriesCartNew = new RepositoriesFactoryCartNew(_context);
            base.Repositories = _repositoriesCartNew;
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
                    if (this._repositoriesCartNew != null)
                    {
                        _repositoriesCartNew.Dispose();
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

        public new IRepositoriesFactoryCartNew Repositories
        {
            get { return _repositoriesCartNew; }

        }

        ContextMainCartNew ContextCartNew
        {
            get { return _context; }
        }

    }
}
