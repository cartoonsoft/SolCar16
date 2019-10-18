using System;
using System.Collections.Generic;
using System.Linq;
using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.Repositories;
using Domain.Core.Interfaces.Repositories;
using Infra.Data.CartNew.Context;
using Infra.Data.CartNew.Repositories.DbCartNew;
using Infra.Data.CartNew.Repositories.Identity;
using Infra.Data.Core.Repositories;

namespace Infra.Data.CartNew.Factory
{
    public class RepositoriesFactoryCartNew : RepositoriesFactoryBase, IRepositoriesFactoryCartNew
    {
        private readonly ContextMainCartNew _context;
        private Dictionary<Type, object> _repositories;

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="context"></param>
        public RepositoriesFactoryCartNew(ContextMainCartNew context): base(context)
        {
            //
            this._context = context;
            _repositories = new Dictionary<Type, object>();
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
                    _repositories = null;
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.
                _repositories = null;

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
                if (_repositories.Keys.Contains(typeof(T)))
                {
                    repository = _repositories[typeof(T)] as IRepositoryBaseReadWrite<T>;
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
                        repository = new RepositoryPessoaCartNew(this._context);
                    }
                    if (typeof(T).Equals(typeof(ModeloDoc)))
                    {
                        repository = new RepositoryModeloDoc(this._context);
                    }
                    if (typeof(T).Equals(typeof(Ato)))
                    {
                        repository = new RepositoryAto(this._context);
                    }
                    if (typeof(T).Equals(typeof(LogModeloDoc)))
                    {
                        repository = new RepositoryLogModeloDoc(this._context);
                    }
                    if (typeof(T).Equals(typeof(TipoAto)))
                    {
                        repository = new RepositoryTipoAto(this._context);
                    }
                    if (typeof(T).Equals(typeof(Acao)))
                    {
                        repository = new RepositoryAcao(this._context);
                    }

                    if (repository != null)
                    {
                        _repositories.Add(typeof(T), repository);
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

        public IRepositoryPessoaCartNew RepositoryPessoaCartNew
        {
            get { return GetRepositoryInstance<PessoaCartNew>() as RepositoryPessoaCartNew; }
        }

        public IRepositoryModeloDoc RepositoryModeloDocx
        {
            get { return GetRepositoryInstance<ModeloDoc>() as RepositoryModeloDoc; }
        }

        public IRepositoryAto RepositoryAto
        {
            get { return GetRepositoryInstance<Ato>() as RepositoryAto; }
        }

        public IRepositoryLogModeloDoc RepositoryLogModeloDocx
        {
            get { return GetRepositoryInstance<LogModeloDoc>() as RepositoryLogModeloDoc; }
        }

        public IRepositoryTipoAto RepositoryTipoAto
        {
            get { return GetRepositoryInstance<TipoAto>() as RepositoryTipoAto; }
        }

        public IRepositoryAcao RepositoryAcao
        {
            get { return GetRepositoryInstance<Acao>() as RepositoryAcao; }
        }
    }
}
