﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cartorio.Entities;
using Domain.Car16.Entities.Car16;
using Domain.Car16.Entities.Car16New;
using Domain.Core.Interfaces.Repositories;

namespace Domain.Cartorio.Interfaces.Repositories
{
    public interface IRepositoryPREIMO : IRepositoryBaseRead<PREIMO>
    {
        PREIMO BuscaDadosImovel(long? numeroPrenotacao = null, long? numeroMatricula = null);

    }
}
