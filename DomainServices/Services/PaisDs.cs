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
    public class PaisDs : DomainServiceCartorioNew<Pais>, IPaisDs
    {
        private readonly IRepositoryPais _repositoryPais = null;

        public PaisDs(IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartorioNew UfwCartNew) : base(UfwCart, UfwCartNew)
        {
            _repositoryPais = this.UfwCartNew.Repositories.RepositoryPais;
            
        }

        public IEnumerable<Pais> BuscarPorNome(string nome)
        {
            return _repositoryPais.BuscarPorNome(nome);
        }
    }
}
