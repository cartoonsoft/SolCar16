using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.Interfaces.Repositories;
using Infra.Data.Car16.Context;
using Infra.Data.Car16.Repositories.Base;

namespace Infra.Data.Car16.Repositories.DbCar16New
{
    public class RepositoryPais : RepositoryBaseReadWrite<Pais>, IRepositoryPais
    {
        private readonly ContextMainCar16New _contextRepository;

        public RepositoryPais(ContextMainCar16New contextRepository) : base(contextRepository)
        {
            _contextRepository = contextRepository;
        }

        public IEnumerable<Pais> BuscarPorNome(string nome)
        {
            //todo: ronaldo, não fiz ainda
            throw new NotImplementedException();
        }
    }
}
