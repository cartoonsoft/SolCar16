using Domain.Core.Interfaces.Data;
using Domain.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Car16.Repositories.Base
{
    public abstract class RepositoriesBase
    {
        protected readonly IContextCore _context;

        private Dictionary<Type, object> GenericRepositories = new Dictionary<Type, object>();

        public RepositoriesBase(IContextCore contextCore)
        {
            _context = contextCore;
        }

        public IRepositoryBase<TEntity> GenericRepository<TEntity>() where TEntity : class
        {
            this.VerifyContext();

            IRepositoryBase<TEntity> repository = null;

            if (GenericRepositories.Keys.Contains(typeof(TEntity)))
            {
                repository = GenericRepositories[typeof(TEntity)] as IRepositoryBase<TEntity>;
            }
            else
            {
                repository = new RepositoryBase<TEntity>(_context);
            }

            if (repository != null)
            {
                GenericRepositories.Add(typeof(TEntity), repository);
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
