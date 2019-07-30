using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cart.Interfaces.UnitOfWork;
using Domain.Core.DomainServices;
using DomainServ.Cart.Interfaces.Base;
using DomainServ.Cart.Interfaces.Factory;

namespace DomainServ.Cart.Factory
{
    public class DomainServicesFactoryCartorio : DomainServicesFactoryBase, IDomainServicesFactoryCartorio
    {
        private readonly IUnitOfWorkDataBaseCartorio _ufwCart;
        private Dictionary<Type, object> DomainServices = new Dictionary<Type, object>();

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="context"></param>
        public DomainServicesFactoryCartorio(IUnitOfWorkDataBaseCartorio UfwCart) : base(UfwCart)
        {
            //
            this._ufwCart = UfwCart;
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
                    domainService = DomainServices[typeof(T)] as IDomainServiceCartorio<T>;
                }
                else
                {
                    //if (typeof(T).Equals(typeof(Pais)))
                    //{
                    //    domainService = new PaisDs(this._ufwCart, this._ufwCartNew);
                    //}

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
    }
}
