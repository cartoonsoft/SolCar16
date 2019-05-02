using AppServices.Car16.AppServices.Base;
using AppServices.Car16.Interfaces;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.Interfaces.UnitOfWork;
using Dto.Car16.Entities.Cadastros;
using LibFunctions.Functions;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Car16.AppServices
{
    public class AppServiceMatriculaAto : AppServiceCar16<DtoMatriculaAto, MatriculaAto>, IAppServiceMatriculaAto
    {
        public AppServiceMatriculaAto(IUnitOfWorkCar16 unitOfWorkCar16) : base(unitOfWorkCar16)
        {
        }

        
    }
}
