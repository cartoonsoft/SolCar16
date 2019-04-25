﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.Entities.Diversas;
using Domain.Core.Interfaces.Repositories;

namespace Domain.Car16.Interfaces.Repositories
{
    public interface IRepositoryArquivoModeloDocx : IRepositoryBaseRead<ArquivoModeloDocx>
    {
        //
        IEnumerable<ArquivoModeloDocxList> ListarArquivoModeloDocx(long? IdTipoAto = null);

    }
}