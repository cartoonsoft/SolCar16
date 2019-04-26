using AppServices.Car16.Interfaces.Base;
using Domain.Car16.Entities.Car16New;
using Dto.Car16.Entities.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Car16.Interfaces
{
    public interface IAppServiceMatriculaAto : IAppServiceCar16<DtoMatriculaAto, MatriculaAto>
    {
        bool EscreverAtoNoWord(DtoMatriculaAto modelo, int irParaFicha, float quantidadeCentrimetrosDaBorda, bool irParaVerso, string filePath);
    }
}
