﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.CartNew.Entities;
using DomainServ.CartNew.Interfaces.Base;
using Dto.CartNew.Entities.Cart_11RI.Diversos;

namespace DomainServ.CartNew.Interfaces
{
    public interface IAcoesUsuariosDs : IDomainServiceCartNew<Acao>
    {
        IEnumerable<DtoMenuAcaoList> GetListMenuUsuario(UsuarioIdentity usr, long IdCtaAcessoSist);
    }
}
