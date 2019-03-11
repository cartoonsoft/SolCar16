using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Car16.AppServices.Base;
using AppServices.Car16.Interfaces;
using Domain.Car16.Entities;
using Domain.Car16.Interfaces.UnitOfWork;
using Dto.Car16.Entities.Cadastros;

namespace AppServices.Car16.AppServices
{
    public class AppServicePais : AppServiceBase<DtoPaisModel, Pais>, IAppServicePais
    {
        public AppServicePais(IUnitOfWorkCar16 unitOfWork) : base(unitOfWork)
        {
            //
        }

        public IEnumerable<DtoPaisModel> BuscarPorNome(string nome)
        {
            throw new NotImplementedException();
        }
    }
}
