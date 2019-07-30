using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.Repositories;
using Domain.CartNew.Interfaces.UnitOfWork;
using DomainServ.CartNew.Base;
using DomainServ.CartNew.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServ.CartNew.Services
{
    public class UfDs : DomainServiceCartorioNew<Uf> , IUfDs
    {
        private readonly IRepositoryUf _repositoryUf;

        public UfDs(IUnitOfWorkDataBaseCartorioNew UfwCartNew) : base(UfwCartNew)
        {
            _repositoryUf = this.UfwCartNew.Repositories.RepositoryUf;
        }
    }
}
