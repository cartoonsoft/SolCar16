using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppServices.Cartorio.Interfaces.Base;
using Domain.Cartorio.Entities.CartorioNew;
using Dto.Cartorio.Entities.Cadastros;
using Dto.Car16.Entities.Cadastros;
using Domain.Cartorio.Entities.Cartorio;
using Dto.Car16.Entities.Diversos;

namespace AppServices.Cartorio.Interfaces
{
    public interface IAppServiceAcesso : IAppServiceCartorio<DtoAcesso, ACESSO>
    {
        DtoExcuteService AddUsrAcesso(long IdAcesso, string IdUsuario);
        DtoExcuteService RemoveUsrAcesso(long IdAcesso, string IdUsuario);
    }
}
