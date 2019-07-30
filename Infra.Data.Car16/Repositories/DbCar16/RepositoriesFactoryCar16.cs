using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Cart.Entities;
using Domain.Cart.Interfaces.Repositories;
using Domain.Core.Interfaces.Repositories;
using Infra.Data.Cartorio.Context;
using Infra.Data.Cartorio.Repositories.Base;

namespace Infra.Data.Cartorio.Repositories.DbCartorio
{
    public class RepositoriesFactoryCartorio : RepositoriesFactoryBase, IRepositoriesFactoryCartorio
    {
        private readonly ContextMainCartorio _context;

        private Dictionary<Type, object> Repositories = new Dictionary<Type, object>();

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="context"></param>
        public RepositoriesFactoryCartorio(ContextMainCartorio context): base(context)
        {
            this._context = context;
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
                    Repositories = null;
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.
                Repositories = null;

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

        private Object GetRepositoryInstance<T>() where T: class
        {
            this.VerifyContext();
            Object repository = null;

            try
            {
                if (Repositories.Keys.Contains(typeof(T)))
                {
                    repository = Repositories[typeof(T)] as IRepositoryBaseRead<T>;
                }
                else
                {
                    if (typeof(T).Equals(typeof(PREIMO)))
                    {
                        repository = new RepositoryPREIMO(this._context);
                    }

                    if (repository != null)
                    {
                        Repositories.Add(typeof(T), repository);
                    }
                }

                if (repository == null)
                {
                    throw new NullReferenceException("repositório base old é nulo!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na criação de repositorio, tipo: " + typeof(T).Name +" "+ ex.Message);
            }

            return repository; 
        }

        public IRepositoryPREIMO RepositoryPREIMO
        {
            get {
                return GetRepositoryInstance<PREIMO>() as RepositoryPREIMO;
            }
        }

    }
}
