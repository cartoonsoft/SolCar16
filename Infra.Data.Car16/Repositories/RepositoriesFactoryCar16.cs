﻿using Domain.Car16.Entities;
using Domain.Car16.Entities.Car16;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.Interfaces.Repositories;
using Domain.Core.Entities.Base;
using Domain.Core.Interfaces.Data;
using Domain.Core.Interfaces.Entities;
using Domain.Core.Interfaces.Repositories;
using Infra.Data.Car16.Context;
using Infra.Data.Car16.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Car16.Repositories
{
    public class RepositoriesFactoryCar16 : RepositoriesFactoryBase, IRepositoriesFactoryCar16
    {
        private readonly ContextMainCar16 _context;
        private Dictionary<Type, object> Repositories = new Dictionary<Type, object>();

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="context"></param>
        public RepositoriesFactoryCar16(ContextMainCar16 context): base(context)
        {
            //
            this._context = context;
        }

        private bool disposedValue = false; // To detect redundant calls

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                    Repositories = null;
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.
                Repositories = null;

                disposedValue = true;
            }

            base.Dispose(disposing);
        }

        private Object GetRepositoryInstance<T>() where T: class
        {
            this.VerifyContext();
            Object repository = null;

            try
            {
                if (Repositories.Keys.Contains(typeof(T)))
                {
                    repository = Repositories[typeof(T)] as IRepositoryBaseReadWrite<T>;
                }
                else
                {
                    if (typeof(T).Equals(typeof(Pais)))
                    {
                        repository = new RepositoryPais(this._context);
                    }

                    if (typeof(T).Equals(typeof(Uf)))
                    {
                        repository = new RepositoryUf(this._context);
                    }
                    if (typeof(T).Equals(typeof(Municipio)))
                    {
                        repository = new RepositoryMunicipio(this._context);
                    }
                    if (typeof(T).Equals(typeof(Pessoa)))
                    {
                        repository = new RepositoryPessoa(this._context);
                    }
                    if (typeof(T).Equals(typeof(ArquivoModeloDocx)))
                    {
                        repository = new RepositoryArquivoModeloDocx(this._context);
                    }
                    if (typeof(T).Equals(typeof(PREIMO)))
                    {
                        repository = new RepositoryPREIMO(this._context);
                    }
                    if (typeof(T).Equals(typeof(PESXPRE)))
                    {
                        repository = new RepositoryPESXPRE(this._context);
                    }
                    if (typeof(T).Equals(typeof(Ato)))
                    {
                        repository = new RepositoryAto(this._context);
                    }
                }

                if (repository != null)
                {
                    Repositories.Add(typeof(T), repository);
                }
                else
                {
                    throw new NullReferenceException("repositório é nulo!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na criação de repositorio, tipo: " + typeof(T).Name +" "+ ex.Message);
            }

            return repository; // new TRepository();
        }

        public IRepositoryPais RepositoryPais
        {
            get {
                return GetRepositoryInstance<Pais>() as RepositoryPais;
            }
        }

        public IRepositoryMunicipio RepositoryMunicipio
        {
            get
            {
                return GetRepositoryInstance<Municipio>() as RepositoryMunicipio;
            }
        }

        public IRepositoryUf RepositoryUf
        {
            get
            {
                return GetRepositoryInstance<Uf>() as RepositoryUf;
            }
        }

        public IRepositoryPessoa RepositoryPessoa
        {
            get { return GetRepositoryInstance<Pessoa>() as RepositoryPessoa; }
        }

        public IRepositoryArquivoModeloDocx RepositoryArquivoModeloDocx
        {
            get { return GetRepositoryInstance<ArquivoModeloDocx>() as RepositoryArquivoModeloDocx; }
        }

        public IRepositoryPREIMO RepositoryPREIMO
        {
            get { return GetRepositoryInstance<PREIMO>() as RepositoryPREIMO; }
        }

        public IRepositoryPESXPRE RepositoryPESXPRE
        {
            get { return GetRepositoryInstance<PESXPRE>() as RepositoryPESXPRE; }
        }

        public IRepositoryAto RepositoryAto
        {
            get { return GetRepositoryInstance<Ato>() as RepositoryAto; }
        }
    }
}
