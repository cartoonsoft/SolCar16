using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Car16.Interfaces.Base;
using Domain.Car16.Entities.Diversas;
using Dto.Car16.Entities.Cadastros;

namespace AppServices.Car16.Interfaces
{
    public interface IAppServiceCadastroAto : IAppServiceCar16<DtoCadastroDeAto,CadastroDeAto>
    {
        bool EscreverAtoNoWord(DtoCadastroDeAto modelo, string filePath, long numSequenciaAto);
    }
}
