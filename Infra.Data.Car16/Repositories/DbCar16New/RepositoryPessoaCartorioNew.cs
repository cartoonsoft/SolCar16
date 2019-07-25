using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities.Car16New;
using Domain.Cartorio.Interfaces.Repositories;
using Infra.Data.Cartorio.Context;
using Infra.Data.Cartorio.Repositories.Base;

namespace Infra.Data.Cartorio.Repositories.DbCartorioNew
{
    public class RepositoryPessoaCartorioNew : RepositoryBaseReadWrite<PessoaCartorioNew>, IRepositoryPessoaCartorioNew
    {
        private readonly ContextMainCartorioNew _contextRepository;

        public RepositoryPessoaCartorioNew(ContextMainCartorioNew contextRepository) : base(contextRepository)
        {
            _contextRepository = contextRepository;
        }

        public PessoaCartorioNew BuscarPorCPFCNPJ(string CpfCnpj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PessoaCartorioNew> BuscarPorNome(string nome)
        {
            throw new NotImplementedException();
        }

    }
}
