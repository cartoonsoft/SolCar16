using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Interfaces.UnitOfWork;
using Domain.Core.DomainServices;
using DomainServ.CartNew.Interfaces.Base;

namespace DomainServ.CartNew.Base
{
    public class DomainServiceCartNew<TEntity> : DomainServiceBase<TEntity>, IDomainServiceCartNew<TEntity>  where TEntity : class
    {
        private readonly IUnitOfWorkDataBaseCartNew _ufwCartNew;

        //caminh do arquivo: errolog.txt
        private readonly string _pathErroLog = string.Empty;


        public DomainServiceCartNew(IUnitOfWorkDataBaseCartNew UfwCartNew, string pathErroLog = null) : base(UfwCartNew)
        {
            _ufwCartNew = UfwCartNew;
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
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.

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

        
        public IUnitOfWorkDataBaseCartNew UfwCartNew
        {
            get { return _ufwCartNew; }
        }

        protected string PathErroLog
        {
            get { return this._pathErroLog; }
        }

    }
}
