using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Core.Interfaces.Data
{
    public interface IContextCore: IDisposable, IObjectContextAdapter
    {
        Database Database { get; }
        DbChangeTracker ChangeTracker { get; }
        DbContextConfiguration Configuration { get; }
        DbEntityEntry Entry(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        bool Equals(object obj);
        int GetHashCode();
        Type GetType();
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
        DbSet Set(Type entityType);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        string ContextName { get; }

    }
}
