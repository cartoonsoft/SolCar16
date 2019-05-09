using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Car16.Interfaces.Base;
using Domain.Car16.Entities.Car16New;
using Dto.Car16.Entities.Cadastros;
using Dto.Car16.Entities.Diversos;

namespace AppServices.Car16.Interfaces
{
    public interface IAppServiceAto : IAppServiceCar16<DtoAto,Ato>
    {
        bool ExisteAtoCadastrado(long numMatricula);

        long? GetNumSequenciaAto(long numMatricula);

        IEnumerable<DtoAtoList> ListarAtos(long? IdTipoAto = null, string IdUsuario = null);

    }
}
