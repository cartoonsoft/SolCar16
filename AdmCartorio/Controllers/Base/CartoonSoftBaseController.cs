using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Domain.Cart.Interfaces.UnitOfWork;
using Domain.CartNew.Interfaces.UnitOfWork;
using Domain.Core.Enumerations;
using Infra.Data.Cartorio.UnitsOfWork.DbCartorio;
using Infra.Data.Cartorio.UnitsOfWork.DbCartorioNew;

namespace AdmCartorio.Controllers.Base
{
    public class CartoonSoftBaseController : Controller
    {
        private readonly IUnitOfWorkDataBaseCartorio     _UfwCart;
        private readonly IUnitOfWorkDataBaseCartorioNew  _UfwCartNew;

        public CartoonSoftBaseController(IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartorioNew UfwCartNew)
        {
            if (UfwCart == null)
            {
                _UfwCart = new UnitOfWorkDataBaseCartorio(BaseDados.DesenvDezesseis);
            }

            if (UfwCartNew == null)
            {
                _UfwCartNew = new UnitOfWorkDataBaseCartorioNew(BaseDados.DesenvDezesseisNew);
            }
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
                    if (_UfwCart != null)
                    {
                        _UfwCart.Dispose();
                    }

                    if (_UfwCartNew != null)
                    {
                        _UfwCartNew.Dispose();
                    }
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.
                disposedValue = true;
            }

            base.Dispose(disposing);
        }

        // override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AppServiceBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }
        public new void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        protected IUnitOfWorkDataBaseCartorio UfwCart
        {
            get { return _UfwCart; }
        }

        protected IUnitOfWorkDataBaseCartorioNew UfwCartNew
        {
            get { return _UfwCartNew; }

        }
    }
}