using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppServices.Cartorio.Interfaces.Base;
using Domain.CartNew.Entities;
using Dto.Car16.Entities.Diversos;
using Dto.Car16.Entities.Cadastros;

namespace AppServices.Cartorio.Interfaces
{
    public interface IAppServiceAcoesUsuarios : IAppServiceCartorio<DtoAcao, Acao>
    {
        DtoExcuteService AddUsrAcesso(long IdAcao, string IdUsuario);

        DtoExcuteService RemoveUsrAcesso(long IdAcao, string IdUsuario);
    }
}
