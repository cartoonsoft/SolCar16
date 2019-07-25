using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppServices.Cartorio.Interfaces.Base;
using Domain.Car16.Entities.Car16New;
using Dto.Cartorio.Entities.Cadastros;
using Dto.Car16.Entities.Cadastros;
using Domain.Car16.Entities.Car16;
using Dto.Car16.Entities.Diversos;

namespace AppServices.Cartorio.Interfaces
{
    public interface IAppServiceAcoesUsuarios : IAppServiceCartorio<DtoAcao, Acao>
    {
        DtoExcuteService AddUsrAcesso(long IdAcao, string IdUsuario);

        DtoExcuteService RemoveUsrAcesso(long IdAcao, string IdUsuario);
    }
}
