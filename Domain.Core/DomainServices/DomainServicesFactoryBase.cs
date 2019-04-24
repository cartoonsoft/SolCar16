/*----------------------------------------------------------------------------
  _____            _                    _____        __ _   
/  __ \          | |                  /  ___|      / _| |  
| /  \/ __ _ _ __| |_ ___   ___  _ __ \ `--.  ___ | |_| |_ 
| |    / _` | '__| __/ _ \ / _ \| '_ \ `--. \/ _ \|  _| __|
| \__/\ (_| | |  | || (_) | (_) | | | /\__/ / (_) | | | |_ 
 \____/\__,_|_|   \__\___/ \___/|_| |_\____/ \___/|_|  \__|
Todos os direitos reservados ®                       
-----------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Core.Entities.Base;
using Domain.Core.Interfaces.Data;
using Domain.Core.Interfaces.DomainServices;
using Domain.Core.Interfaces.UnitOfWork;

namespace Domain.Core.DomainServices
{
    public class DomainServicesFactoryBase : IDomainServicesFactoryBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        private Dictionary<Type, object> GenericDomainServices = new Dictionary<Type, object>();

        public DomainServicesFactoryBase(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                    GenericDomainServices = null;

                    //todo: verificar se deve dar dispose no _unitOfWork aqui
                    //if (_unitOfWork != null)
                    //{
                    //    _unitOfWork.Dispose();
                    //}

                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RepositoriesBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        public IDomainServiceBase<T> GenericDomainService<T>() where T : class
        {
            this.VerifyUnitOfWork();

            IDomainServiceBase<T> domainService = null;

            if (GenericDomainServices.Keys.Contains(typeof(T)))
            {
                domainService = GenericDomainServices[typeof(T)] as IDomainServiceBase<T>;
            }
            else
            {
                domainService = new DomainServiceBase<T>(_unitOfWork);
            }

            if (domainService != null)
            {
                GenericDomainServices.Add(typeof(T), domainService);
            }

            return domainService;
        }

        protected void VerifyUnitOfWork()
        {
            if (this._unitOfWork == null)
            {
                throw new ArgumentNullException("unit of work é nulo. verificar!");
            }
        }

    }
}
