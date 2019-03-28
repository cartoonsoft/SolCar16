using Domain.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Interfaces.Repositories
{
    public interface IRepositoriesBase: IDisposable
    {
        IRepositoryBase<TEntity> GenericRepository<TEntity>() where TEntity : EntityBase;

    }
}
