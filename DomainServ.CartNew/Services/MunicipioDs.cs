using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.Repositories;
using Domain.CartNew.Interfaces.UnitOfWork;
using DomainServ.CartNew.Base;
using DomainServ.CartNew.Interfaces;

namespace DomainServ.CartNew.Services
{
    public class MunicipioDs : DomainServiceCartNew<Municipio>, IMunicipioDs
    {
        private readonly IRepositoryUf _repositoryUf;

        public MunicipioDs(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            _repositoryUf = this.UfwCartNew.Repositories.RepositoryUf;
        }
    }
}
