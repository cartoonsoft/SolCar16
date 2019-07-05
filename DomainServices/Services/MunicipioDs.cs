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
using Dto.Cartorio.Entities.Cadastros;

namespace DomainServices.Services
{
    public class MunicipioDs : DomainServiceCartorioNew<Municipio>, IMunicipioDs
    {

        private readonly IRepositoryUf _repositoryUf;

        public MunicipioDs(IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartorioNew UfwCartNew) : base(UfwCart, UfwCartNew)
        {
            _repositoryUf = this.UfwCartNew.Repositories.RepositoryUf;
        }

    }
}
