using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.enums;
using Domain.Car16.Interfaces.UnitOfWork;
using Infra.Data.Car16.Context;
using Infra.Data.Car16.Repositories.Base;

namespace Infra.Data.Car16.UnitsOfWork
{
    public class UnitOfWorkDataBaseCar16New: UnitOfWorkCar16, IUnitOfWorkDataBaseCar16New
    {

        public UnitOfWorkDataBaseCar16New(BaseDados baseDados, ContextMainCar16 context = null, InfraDataEventLogging log = null): base(baseDados, context, log)
        {
            //
            
        }

        

    }
}
