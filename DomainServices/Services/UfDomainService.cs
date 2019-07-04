using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cartorio.Entities.CartorioNew;
using Domain.Cartorio.Interfaces.Repositories;
using Domain.Cartorio.Interfaces.UnitOfWork;
using DomainServices.Base;
using DomainServices.Interfaces;

namespace DomainServices.Services
{
    public class UfDomainService : DomainServiceCartorioNew<Uf> , IUfDomainService
    {
        private readonly IRepositoryUf _repositoryUf;

        public UfDomainService(IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartorioNew UfwCartNew) : base(ufwCart, UfwCartNew)
        {
            _repositoryUf = this.ufwCartNew.Repositories.RepositoryUf;
        }

    }
}
