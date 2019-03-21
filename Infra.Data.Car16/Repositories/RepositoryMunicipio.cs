﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities;
using Domain.Car16.Interfaces.Repositories;
using Domain.Core.Entities.Base;
using Domain.Core.Interfaces.Repositories;
using Infra.Data.Car16.Context;
using Infra.Data.Car16.Repositories.Base;

namespace Infra.Data.Car16.Repositories
{
    public class RepositoryMunicipio : RepositoryBase<Municipio>, IRepositoryMunicipio
    {
        private readonly ContextMainCar16 _contexRep;

        public RepositoryMunicipio(ContextMainCar16 contexRep): base (contexRep)
        {
            _contexRep = contexRep; 
        }
    }
}
