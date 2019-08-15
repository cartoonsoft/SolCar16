﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.CartNew.Entities;
using AppServ.Core.Interfaces;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;

namespace AppServices.Cartorio.Interfaces
{
    public interface IAppServiceAcoesUsuarios : IAppServiceCartorio<DtoAcao, Acao>
    {
        DtoExcuteService AddUsrAcesso(long IdAcao, string IdUsuario);

        DtoExcuteService RemoveUsrAcesso(long IdAcao, string IdUsuario);

        IEnumerable<DtoMenu> ListaMenuUsuario(UsuarioIdentity usr);

    }
}
