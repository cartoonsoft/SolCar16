using AppServices.Car16.Interfaces.Base;
using Domain.Car16.Entities.Car16;
using Dto.Car16.Entities.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Car16.Interfaces
{
    public interface IAppServicePESSOA : IAppServiceCar16<DtoPESSOA,PESSOA>
    {
        DtoPESSOA GetPESSOA(long ID);
    }
}
