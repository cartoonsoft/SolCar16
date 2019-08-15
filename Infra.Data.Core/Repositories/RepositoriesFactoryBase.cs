using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Core.Entities.Base;
using Domain.Core.Interfaces.Data;
using Domain.Core.Interfaces.Repositories;

namespace Infra.Data.Core.Repositories
{
    public class RepositoriesFactoryBase : IRepositoriesFactoryBase
    {
        private readonly IContextCore _contextCore;
        private Dictionary<Type, object> _genericRepositories; 

        public RepositoriesFactoryBase(IContextCore contextCore)
        {
            _contextCore = contextCore;
            _genericRepositories  = new Dictionary<Type, object>();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                    _genericRepositories = null;
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.

                disposedValue = true;
            }
        }

        // override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RepositoriesBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        public IRepositoryBaseReadWrite<T> GenericRepository<T>() where T : class
        {
            this.VerifyContext();

            IRepositoryBaseReadWrite<T> repository = null;

            if (_genericRepositories.Keys.Contains(typeof(T)))
            {
                repository = _genericRepositories[typeof(T)] as IRepositoryBaseReadWrite<T>;
            }
            else
            {
                repository = new RepositoryBaseReadWrite<T>(_contextCore);

                if (repository != null)
                {
                    _genericRepositories.Add(typeof(T), repository);
                }
            }

            if( repository == null)
            {
                throw new NullReferenceException("repositório Generic é nulo!");
            }

            return repository;
        }

        protected void VerifyContext()
        {
            if (_contextCore == null)
            {
                throw new ArgumentNullException("Contexto é nulo. verificar!");
            }
        }

    }
}
