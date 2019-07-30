using System;
using System.Collections.Generic;
using System.Linq;
using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.Repositories;
using Domain.Core.Interfaces.Repositories;
using Infra.Data.Cartorio.Context;
using Infra.Data.Cartorio.Repositories.Base;

namespace Infra.Data.Cartorio.Repositories.DbCartorioNew
{
    public class RepositoriesFactoryCartorioNew : RepositoriesFactoryBase, IRepositoriesFactoryCartorioNew
    {
        private readonly ContextMainCartorioNew _context;
        private Dictionary<Type, object> Repositories = new Dictionary<Type, object>();

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="context"></param>
        public RepositoriesFactoryCartorioNew(ContextMainCartorioNew context): base(context)
        {
            //
            this._context = context;
        }

        #region IDisposable Support
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

        public new void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

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
                    if (typeof(T).Equals(typeof(PessoaCartNew)))
                    {
                        repository = new RepositoryPessoaCartorioNew(this._context);
                    }
                    if (typeof(T).Equals(typeof(ArquivoModeloDocx)))
                    {
                        repository = new RepositoryArquivoModeloDocx(this._context);
                    }
                    if (typeof(T).Equals(typeof(Ato)))
                    {
                        repository = new RepositoryAto(this._context);
                    }
                    if (typeof(T).Equals(typeof(LogArquivoModeloDocx)))
                    {
                        repository = new RepositoryLogArquivoModeloDocx(this._context);
                    }

                    if (repository != null)
                    {
                        Repositories.Add(typeof(T), repository);
                    }
                }

                if(repository == null)
                {
                    throw new NullReferenceException("repositório New é nulo!");
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

        public IRepositoryPessoaCartNew RepositoryPessoa
        {
            get { return GetRepositoryInstance<PessoaCartNew>() as RepositoryPessoaCartorioNew; }
        }

        public IRepositoryArquivoModeloDocx RepositoryArquivoModeloDocx
        {
            get { return GetRepositoryInstance<ArquivoModeloDocx>() as RepositoryArquivoModeloDocx; }
        }

        public IRepositoryAto RepositoryAto
        {
            get { return GetRepositoryInstance<Ato>() as RepositoryAto; }
        }
        public IRepositoryLogArquivoModeloDocx RepositoryLogArquivoModeloDocx
        {
            get { return GetRepositoryInstance<LogArquivoModeloDocx>() as RepositoryLogArquivoModeloDocx; }
        }

    }
}
