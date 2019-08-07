using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.Repositories;
using Infra.Data.CartNew.Context;
using Infra.Data.Core.Repositories;

namespace Infra.Data.CartNew.Repositories.DbCartNew
{
    public class RepositoryPais : RepositoryBaseReadWrite<Pais>, IRepositoryPais
    {
        private readonly ContextMainCartNew _contextRepository;

        public RepositoryPais(ContextMainCartNew contextRepository) : base(contextRepository)
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
