using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cart.Entities;
using Domain.CartNew.Interfaces.Repositories;
using Infra.Data.Cartorio.Context;
using Infra.Data.Cartorio.Repositories.Base;

namespace Infra.Data.Cartorio.Repositories
{
    public class RepositoryPessoaCartorio : RepositoryBaseReadWrite<PessoaCart>, IRepositoryPessoaCart
    {
        private readonly ContextMainCartorio _contexRepository;

        public RepositoryPessoaCartorio(ContextMainCartorio contexRepository) : base(contexRepository)
        {
            _contexRepository = contexRepository;
        }

    }
}
