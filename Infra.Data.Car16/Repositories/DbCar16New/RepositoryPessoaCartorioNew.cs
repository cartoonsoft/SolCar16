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
    public class RepositoryPessoaCartorioNew : RepositoryBaseReadWrite<PessoaCartNew>, IRepositoryPessoaCartNew
    {
        private readonly ContextMainCartorioNew _contextRepository;

        public RepositoryPessoaCartorioNew(ContextMainCartorioNew contextRepository) : base(contextRepository)
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
