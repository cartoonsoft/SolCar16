using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.Interfaces.DomainServices;
using Domain.Car16.Interfaces.DomainServices.Base;
using Domain.Car16.Interfaces.UnitOfWork;
using Domain.Core.DomainServices;

namespace Domain.Car16.DomainServices.Base
{
    public class DomainServicesFactoryCar16 : DomainServicesFactoryBase, IDomainServicesFactoryCar16
    {
        private readonly IUnitOfWorkCar16 _unitOfWorkCar16;
        private Dictionary<Type, object> DomainServices = new Dictionary<Type, object>();

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="context"></param>
        public DomainServicesFactoryCar16(IUnitOfWorkCar16 unitOfWorkCar16) : base(unitOfWorkCar16)
        {
            //
            this._unitOfWorkCar16 = unitOfWorkCar16;
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

                    //todo: Ronaldo, verificar se deve dar dispose no _unitOfWorkCar16
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
                    domainService = DomainServices[typeof(T)] as IDomainServiceCar16<T>;
                }
                else
                {
                    if (typeof(T).Equals(typeof(Pais)))
                    {
                        domainService = new PaisDomainService(this._unitOfWorkCar16);
                    }

                    if (typeof(T).Equals(typeof(Uf)))
                    {
                        domainService = new UfDomainService(this._unitOfWorkCar16);
                    }
                    if (typeof(T).Equals(typeof(Municipio)))
                    {
                        domainService = new MunicipioDomainService(this._unitOfWorkCar16);
                    }
                    if (typeof(T).Equals(typeof(Pessoa)))
                    {
                        domainService = new PessoaDomainService(this._unitOfWorkCar16);
                    }
                    if (typeof(T).Equals(typeof(ArquivoModeloDocx)))
                    {
                        domainService = new ArquivoModeloDocxDomainService(this._unitOfWorkCar16);
                    }
                }

                if (domainService != null)
                {
                    DomainServices.Add(typeof(T), domainService);
                }
                else
                {
                    throw new NullReferenceException("Domainservice é nulo!");
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
            get { return GetDomainServiceInstance<Pessoa>() as IPessoaDomainService; }
        }

        public IArquivoModeloDocxDomainService ArquivoModeloDocxDomainService
        {
            get { return GetDomainServiceInstance<ArquivoModeloDocx>() as IArquivoModeloDocxDomainService; }
        }
    }
}
