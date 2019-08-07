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
    public class RepositoryPessoaCartNew : RepositoryBaseReadWrite<PessoaCartNew>, IRepositoryPessoaCartNew
    {
        private readonly ContextMainCartNew _contextRepository;

        public RepositoryPessoaCartNew(ContextMainCartNew contextRepository) : base(contextRepository)
        {
            _contextRepository = contextRepository;
        }

        public PessoaCartNew BuscarPorCPFCNPJ(string CpfCnpj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PessoaCartNew> BuscarPorNome(string nome)
        {
            throw new NotImplementedException();
        }

    }
}
