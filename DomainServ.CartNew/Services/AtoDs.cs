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
    public class AtoDs : DomainServiceCartorioNew<Ato>, IAtoDs
    {
        private readonly IRepositoryAto _repositoryAto;

        public AtoDs(IUnitOfWorkDataBaseCartorioNew UfwCartNew) : base(UfwCartNew)
        {
            _repositoryAto = UfwCartNew.Repositories.RepositoryAto;
        }

        public bool CadastrarAto(Ato ato)
        {
            try
            {
                _repositoryAto.Add(ato);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool ExisteAtoCadastrado(long numMatricula)
        {
            throw new NotImplementedException();
        }

        public Docx GetUltimaFichaGravada(string NumMatricula)
        {
            throw new NotImplementedException();
        }

        public short GetUltimoNumFicha(string NumMatricula)
        {
            throw new NotImplementedException();
        }
    }
}
