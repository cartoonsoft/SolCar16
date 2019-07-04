﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainServices.Base;
using DomainServices.Interfaces;
using Domain.Cartorio.Entities.CartorioNew;
using Domain.Cartorio.Interfaces.Repositories;
using Domain.Cartorio.Interfaces.UnitOfWork;

namespace DomainServices.Services
{
    public class AtoDomainService : DomainServiceCartorioNew<Ato>, IAtoDomainService
    {
        private readonly IRepositoryAto _repositoryAto;

        public AtoDomainService(IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartorioNew UfwCartNew) : base(ufwCart, UfwCartNew)
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
