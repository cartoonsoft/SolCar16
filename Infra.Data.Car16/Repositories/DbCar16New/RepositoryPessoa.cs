﻿using System;
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
    public class RepositoryPessoa : RepositoryBaseReadWrite<Pessoa>, IRepositoryPessoa
    {
        private readonly ContextMainCar16New _contextRepository;

        public RepositoryPessoa(ContextMainCar16New contextRepository) : base(contextRepository)
        {
            _contextRepository = contextRepository;
        }

        public Pessoa BuscarPorCPFCNPJ(string CpfCnpj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pessoa> BuscarPorNome(string nome)
        {
            throw new NotImplementedException();
        }

    }
}
