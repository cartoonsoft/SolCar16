using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Interfaces.Repositories
{
    public interface IRepositoriesBase
    {
        IRepositoryBase<TEntity> GenericRepository<TEntity>() where TEntity : class;

    }
}
