using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Base;

namespace AppServ.Core.Interfaces
{
    public interface IAppServiceCartorio<TDtoEntityModel, TEntity>: IAppServiceBase<TDtoEntityModel, TEntity> where TDtoEntityModel : class where TEntity : class
    {
        //
    }
}
