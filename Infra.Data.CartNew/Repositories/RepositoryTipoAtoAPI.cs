﻿using Domain.Car16.Entities.API;
using Domain.Car16.Interfaces.Repositories;
using Domain.Core.Interfaces.Data;
using Infra.Data.Car16.Context;
using Infra.Data.Car16.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Car16.Repositories
{
    public class RepositoryTipoAtoAPI 
        : RepositoryBase<TipoAtoAPI>, IRepositoryTipoAtoAPI
    {
        private readonly ContextMainCar16 _contexRep;

        public RepositoryTipoAtoAPI(ContextMainCar16 context) : base(context)
        {
            _contexRep = context;
        }
    }
}
