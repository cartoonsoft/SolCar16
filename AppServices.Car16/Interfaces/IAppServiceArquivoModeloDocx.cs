using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppServices.Car16.Interfaces.Base;
using Domain.Car16.Entities.Car16New;
using Dto.Car16.Entities.Cadastros;
using Dto.Car16.Entities.Diversos;

namespace AppServices.Car16.Interfaces
{
    public interface IAppServiceArquivoModeloDocx: IAppServiceCar16<DtoArquivoModeloDocxModel, ArquivoModeloDocx>
    {
        //
        void SalvarModelo(DtoArquivoModeloDocxModel dtoArq, string IdUsuario);

        IEnumerable<DtoArquivoModeloDocxList> ListarArquivoModeloDocx(long? IdTipoAto = null);

    }
}
