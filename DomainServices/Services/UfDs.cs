using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities.Car16New;
using Domain.Cartorio.Interfaces.Repositories;
using Domain.Cartorio.Interfaces.UnitOfWork;
using DomainServices.Base;
using DomainServices.Interfaces;

namespace DomainServices.Services
{
    public class UfDs : DomainServiceCartorioNew<Uf> , IUfDs
    {
        private readonly IRepositoryUf _repositoryUf;

        public UfDs(IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartorioNew UfwCartNew) : base(UfwCart, UfwCartNew)
        {
            _repositoryUf = this.UfwCartNew.Repositories.RepositoryUf;
        }

    }
}
