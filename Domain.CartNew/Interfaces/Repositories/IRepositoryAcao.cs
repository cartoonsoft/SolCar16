﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Entities;
using Domain.CartNew.Entities.Diversos;
using Domain.Core.Interfaces.Repositories;

namespace Domain.CartNew.Interfaces.Repositories
{
    public interface IRepositoryAcao : IRepositoryBaseReadWrite<Acao>
    {
        IEnumerable<MenuAcaoList> GetListMenuUsuario(string IdUsuario, long IdCtaAcessoSist);

    }
}
