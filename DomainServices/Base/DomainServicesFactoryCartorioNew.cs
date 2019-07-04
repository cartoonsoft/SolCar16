using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cartorio.Entities.Cartorio;
using Domain.Cartorio.Entities.CartorioNew;
using Domain.Cartorio.Interfaces.UnitOfWork;
using Domain.Core.DomainServices;
using DomainServices.Interfaces;
using DomainServices.Services;

namespace DomainServices.Base
{
    public class DomainServicesFactoryCartorioNew : DomainServicesFactoryBase, IDomainServicesFactoryCartorioNew
    {
        private readonly IUnitOfWorkDataBaseCartorioNew _ufwCartNew;
        private readonly IUnitOfWorkDataBaseCartorio _ufwCart;
        private Dictionary<Type, object> DomainServices = new Dictionary<Type, object>();

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="context"></param>
        public DomainServicesFactoryCartorioNew(IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartorioNew UfwCartNew) : base(UfwCartNew)
        {
            //
            this._ufwCart = UfwCart;
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
                        domainService = new PaisDomainService(this._ufwCart, this._ufwCartNew);
                    }

                    if (typeof(T).Equals(typeof(Uf)))
                    {
                        domainService = new UfDomainService(this._ufwCart, this._ufwCartNew);
                    }
                    if (typeof(T).Equals(typeof(Municipio)))
                    {
                        domainService = new MunicipioDomainService(this._ufwCart, this._ufwCartNew);
                    }
                    if (typeof(T).Equals(typeof(PessoaCartorioNew)))
                    {
                        domainService = new PessoaDomainService(this._ufwCart, this._ufwCartNew );
                    }
                    if (typeof(T).Equals(typeof(ArquivoModeloDocx)))
                    {
                        domainService = new ArquivoModeloDocxDomainService(this._ufwCart, this._ufwCartNew);
                    }
                    if (typeof(T).Equals(typeof(Ato)))
                    {
                        domainService = new AtoDomainService(this._ufwCart, this._ufwCartNew);
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

        public IPaisDomainService PaisDomainService
        {
            get { return GetDomainServiceInstance<Pais>() as IPaisDomainService; }

        }
        
        public IUfDomainService UfDomainService
        {
            get { return GetDomainServiceInstance<Uf>() as IUfDomainService; }
        }

        public IMunicipioDomainService MunicipioDomainService
        {
            get { return GetDomainServiceInstance<Municipio>() as IMunicipioDomainService; }
        }

        public IPessoaDomainService PessoaDomainService
        {
            get { return GetDomainServiceInstance<PessoaCartorio>() as IPessoaDomainService; }
        }

        public IArquivoModeloDocxDomainService ArquivoModeloDocxDomainService
        {
            get { return GetDomainServiceInstance<ArquivoModeloDocx>() as IArquivoModeloDocxDomainService; }
        }

        public IAtoDomainService AtoDomainService {
            get { return GetDomainServiceInstance<Ato>() as IAtoDomainService; }
        } 
    }
}
