using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Cartorio.AppServices.Base;
using Domain.Core.Entities.Base;

namespace AppServices.Cartorio.Interfaces.Base
{
    public interface IAppServiceCartorio<TDtoEntityModel, TEntity>: IAppServiceBase<TDtoEntityModel, TEntity> where TDtoEntityModel : class where TEntity : class
    {
         
    }
}
