using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.UnitOfWork;
using Domain.Core.DomainServices;
using DomainServ.CartNew.Interfaces;
using DomainServ.CartNew.Interfaces.Base;
using DomainServ.CartNew.Interfaces.Factory;
using DomainServ.CartNew.Services;

namespace DomainServ.CartNew.Factory
{
    public class DomainServicesFactoryCartorioNew : DomainServicesFactoryBase, IDomainServicesFactoryCartorioNew
    {
        private readonly IUnitOfWorkDataBaseCartorioNew _ufwCartNew;
        private Dictionary<Type, object> DomainServices = new Dictionary<Type, object>();

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="context"></param>
        public DomainServicesFactoryCartorioNew(IUnitOfWorkDataBaseCartorioNew UfwCartNew) : base(UfwCartNew)
        {
            //
            this._ufwCartNew = UfwCartNew;
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
                    DomainServices = null;

                    //todo: Ronaldo, verificar se deve dar dispose no _ufwCart
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.

                disposedValue = true;
            }

            base.Dispose(disposing);
        }

        // override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DomainServiceBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public new void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        private Object GetDomainServiceInstance<T>() where T : class
        {
            this.VerifyUnitOfWork();
            Object domainService = null;

            try
            {
                if (DomainServices.Keys.Contains(typeof(T)))
                {
                    domainService = DomainServices[typeof(T)] as IDomainServiceCartorioNew<T>;
                }
                else
                {
                    if (typeof(T).Equals(typeof(Pais)))
                    {
                        domainService = new PaisDs(this._ufwCartNew);
                    }

                    if (typeof(T).Equals(typeof(Uf)))
                    {
                        domainService = new UfDs(this._ufwCartNew);
                    }
                    if (typeof(T).Equals(typeof(Municipio)))
                    {
                        domainService = new MunicipioDs(this._ufwCartNew);
                    }
                    if (typeof(T).Equals(typeof(PessoaCartNew)))
                    {
                        domainService = new PessoaCartNewDs(this._ufwCartNew );
                    }
                    if (typeof(T).Equals(typeof(ArquivoModeloDocx)))
                    {
                        domainService = new ArquivoModeloDocxDs(this._ufwCartNew);
                    }
                    if (typeof(T).Equals(typeof(Ato)))
                    {
                        domainService = new AtoDs(this._ufwCartNew);
                    }

                    if (domainService != null)
                    {
                        DomainServices.Add(typeof(T), domainService);
                    }
                }

                if (domainService == null)
                {
                    throw new NullReferenceException("Domain service é nulo!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na criação de domain service, tipo: " + typeof(T).Name + " " + ex.Message);
            }

            return domainService; // new TRepository();
        }

        public IPaisDs PaisDs
        {
            get { return GetDomainServiceInstance<Pais>() as IPaisDs; }

        }
        
        public IUfDs UfDs
        {
            get { return GetDomainServiceInstance<Uf>() as IUfDs; }
        }

        public IMunicipioDs MunicipioDs
        {
            get { return GetDomainServiceInstance<Municipio>() as IMunicipioDs; }
        }

        public IPessoaCartNewDs PessoaCartNewDs
        {
            get { return GetDomainServiceInstance<PessoaCartNew>() as IPessoaCartNewDs; }
        }

        public IArquivoModeloDocxDs ArquivoModeloDocxDs
        {
            get { return GetDomainServiceInstance<ArquivoModeloDocx>() as IArquivoModeloDocxDs; }
        }

        public IAtoDs AtoDs{
            get { return GetDomainServiceInstance<Ato>() as IAtoDs; }
        } 
    }
}
