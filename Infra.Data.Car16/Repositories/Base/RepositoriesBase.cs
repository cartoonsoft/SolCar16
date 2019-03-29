using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Core.Entities.Base;
using Domain.Core.Interfaces.Data;
using Domain.Core.Interfaces.Repositories;

namespace Infra.Data.Car16.Repositories.Base
{
    public abstract class RepositoriesBase : IRepositoriesBase
    {
        protected readonly IContextCore _context;

        private Dictionary<Type, object> GenericRepositories = new Dictionary<Type, object>();

        public RepositoriesBase(IContextCore contextCore)
        {
            _context = contextCore;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).

                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                GenericRepositories = null;

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RepositoriesBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion


        public IRepositoryBaseReadWrite<T> GenericRepository<T>() where T : EntityBase
        {
            this.VerifyContext();

            IRepositoryBaseReadWrite<T> repository = null;

            if (GenericRepositories.Keys.Contains(typeof(T)))
            {
                repository = GenericRepositories[typeof(T)] as IRepositoryBaseReadWrite<T>;
            }
            else
            {
                repository = new RepositoryBaseReadWrite<T>(_context);
            }

            if (repository != null)
            {
                GenericRepositories.Add(typeof(T), repository);
            }

            return repository;
        }

        protected void VerifyContext()
        {
            if (_context == null)
            {
                throw new ArgumentNullException("Contexto é nulo. verificar!");
            }
        }

    }
}
