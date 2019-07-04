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
    public class PaisDomainService : DomainServiceCartorioNew<Pais>, IPaisDomainService
    {
        private readonly IRepositoryPais _repositoryPais = null;

        public PaisDomainService(IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartorioNew UfwCartNew) : base(ufwCart, UfwCartNew)
        {
            _repositoryPais = this.ufwCartNew.Repositories.RepositoryPais;
            
        }

        public IEnumerable<Pais> BuscarPorNome(string nome)
        {
            return _repositoryPais.BuscarPorNome(nome);
        }
    }
}
