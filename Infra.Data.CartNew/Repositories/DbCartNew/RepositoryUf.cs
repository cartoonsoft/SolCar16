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
    public class RepositoryUf : RepositoryBaseReadWrite<Uf>, IRepositoryUf
    {
        private readonly ContextMainCartNew _contextRepository;

        public RepositoryUf(ContextMainCartNew contextRepository) : base(contextRepository)
        {
            _contextRepository = contextRepository;
        }
    }
}
