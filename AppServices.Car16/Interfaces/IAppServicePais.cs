using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppServices.Car16.Interfaces.Base;
using Domain.Car16.Entities.Car16New;
using Dto.Car16.Entities.Cadastros;

namespace AppServices.Car16.Interfaces
{
    public interface IAppServicePais : IAppServiceCar16<DtoPaisModel, Pais>
    {
        IEnumerable<DtoPaisModel> BuscarPorNome(string nome);
    }
}
