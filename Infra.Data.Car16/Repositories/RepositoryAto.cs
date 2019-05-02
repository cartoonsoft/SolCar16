using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.Interfaces.Repositories;
using Infra.Data.Car16.Context;
using Infra.Data.Car16.Repositories.Base;

namespace Infra.Data.Car16.Repositories
{
    public class RepositoryAto : RepositoryBaseReadWrite<Ato>, IRepositoryAto
    {
        private readonly ContextMainCar16 _contexRep;

        public RepositoryAto(ContextMainCar16 contexRep) : base(contexRep)
        {
            _contexRep = contexRep;
        }
    }
}
