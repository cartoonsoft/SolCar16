using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cartorio.enums;
using Domain.Cartorio.Interfaces.Repositories;
using Domain.Cartorio.Interfaces.UnitOfWork;
using Infra.Data.Cartorio.Context;
using Infra.Data.Cartorio.Repositories.Base;
using Infra.Data.Cartorio.Repositories.DbCartorio;
using Infra.Data.Cartorio.UnitsOfWork.Base;

namespace Infra.Data.Cartorio.UnitsOfWork.DbCartorio
{
    public class UnitOfWorkDataBaseCartorio: UfwCart, IUnitOfWorkDataBaseCartorio
    {
        private ContextMainCartorio _context;
        private readonly IRepositoriesFactoryCartorio _repositoriesCartorio;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="baseDados"></param>
        /// <param name="context"></param>
        /// <param name="log"></param>
        public UnitOfWorkDataBaseCartorio(BaseDados baseDados, ContextMainCartorio context = null, InfraDataEventLogging log = null): base(baseDados, context, log)
        {
            _context = context;

            if (_context == null)
            {
                _context = new ContextMainCartorio(GetContextName(baseDados));
                base.Context = _context;
            }

            _repositoriesCartorio = new RepositoriesFactoryCartorio(_context);
            base.Repositories = _repositoriesCartorio;
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
                    if (this._repositoriesCartorio != null)
                    {
                        _repositoriesCartorio.Dispose();
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

        public new IRepositoriesFactoryCartorio Repositories
        {
            get { return _repositoriesCartorio; }

        }

        ContextMainCartorio ContextCartorio
        {
            get { return _context; }
        }

    }
}
