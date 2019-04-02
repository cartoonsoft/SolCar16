using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Car16.Interfaces.Base;
using Domain.Car16.Interfaces.UnitOfWork;
using Domain.Core.Entities.Base;
using Dto.Car16.Entities.Base;

namespace AppServices.Car16.AppServices.Base
{
    public class AppServiceCar16<TDtoEntityModel, TEntity>: AppServiceBase<TDtoEntityModel, TEntity>, IAppServiceCar16<TDtoEntityModel, TEntity> where TDtoEntityModel : class where TEntity : class
    {
        protected readonly IUnitOfWorkCar16 _unitOfWorkCar16;

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public AppServiceCar16(IUnitOfWorkCar16 unitOfWorkCar16) : base(unitOfWorkCar16)
        {
            this._unitOfWorkCar16 = unitOfWorkCar16;
        }

    }
}
