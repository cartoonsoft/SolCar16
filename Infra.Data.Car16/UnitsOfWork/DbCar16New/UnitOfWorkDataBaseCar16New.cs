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
using Infra.Data.Cartorio.Repositories.DbCartorioNew;
using Infra.Data.Cartorio.UnitsOfWork.Base;

namespace Infra.Data.Cartorio.UnitsOfWork.DbCartorioNew
{
    public class UnitOfWorkDataBaseCartorioNew: UfwCart, IUnitOfWorkDataBaseCartorioNew
    {
        private readonly ContextMainCartorioNew _context;
        private readonly IRepositoriesFactoryCartorioNew _repositoriesCartorioNew;

        public UnitOfWorkDataBaseCartorioNew(BaseDados baseDados, ContextMainCartorioNew context = null, InfraDataEventLogging log = null): base(baseDados, context, log)
        {
            _context = context;

            if (_context == null)
            {
                _context = new ContextMainCartorioNew(GetContextName(baseDados));
                base.Context = _context;
            }

            _repositoriesCartorioNew = new RepositoriesFactoryCartorioNew(_context);
            base.Repositories = _repositoriesCartorioNew;
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
                    if (this._repositoriesCartorioNew != null)
                    {
                        _repositoriesCartorioNew.Dispose();
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

        public new IRepositoriesFactoryCartorioNew Repositories
        {
            get { return _repositoriesCartorioNew; }

        }

        ContextMainCartorioNew ContextCartorioNew
        {
            get { return _context; }
        }

    }
}
