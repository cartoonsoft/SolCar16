using Domain.Core.Entities.Base;
using Dto.Car16.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Car16.Interfaces.Base
{
    public interface IAppServiceCar16<TDtoEntityModel, TEntity> : IAppServiceBase<TDtoEntityModel, TEntity> where TDtoEntityModel : class where TEntity : class
    {
         
    }
}
