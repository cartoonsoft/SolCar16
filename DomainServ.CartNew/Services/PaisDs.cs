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
    public class PaisDs : DomainServiceCartorioNew<Pais>, IPaisDs
    {
        private readonly IRepositoryPais _repositoryPais = null;

        public PaisDs(IUnitOfWorkDataBaseCartorioNew UfwCartNew) : base(UfwCartNew)
        {
            _repositoryPais = this.UfwCartNew.Repositories.RepositoryPais;
            
        }

        public IEnumerable<Pais> BuscarPorNome(string nome)
        {
            return _repositoryPais.BuscarPorNome(nome);
        }
    }
}
