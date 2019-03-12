using AppServices.Car16.AppServices.Base;
using AppServices.Car16.Interfaces;
using Domain.Car16.Entities;
using Domain.Core.Interfaces.UnitOfWork;
using Dto.Car16.Entities.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Car16.AppServices
{
    public class AppServiceArquivoModeloDocx : AppServiceBase<DtoArquivoModeloDocxModel, ArquivoModeloDocx>, IAppServiceArquivoModeloDocx
    {
        public AppServiceArquivoModeloDocx(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            //
        }
    }
}
