using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.Repositories;
using Infra.Data.Cartorio.Context;
using Infra.Data.Cartorio.Repositories.Base;

namespace Infra.Data.Cartorio.Repositories.DbCartorioNew
{
    public class RepositoryPais : RepositoryBaseReadWrite<Pais>, IRepositoryPais
    {
        private readonly ContextMainCartorioNew _contextRepository;

        public RepositoryPais(ContextMainCartorioNew contextRepository) : base(contextRepository)
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
