﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Domain.Cart.Interfaces.UnitOfWork;
using Domain.CartNew.Interfaces.UnitOfWork;
using AdmCartorio.App_Start.Identity;
using AdmCartorio.Models.Identity.Entities;
using Infra.Data.Cartorio.UnitsOfWork;
using Infra.Data.Cartorio.UnitsOfWork.DbCartorio;
using Infra.Data.Cartorio.UnitsOfWork.DbCartorioNew;
using System.Web.Routing;

namespace AdmCartorio.Controllers.Base
{
    public class AdmCartorioBaseController : CartoonSoftBaseController
    {
        private ApplicationUser currentUser;

        public AdmCartorioBaseController(IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartorioNew UfwCartNew): base(UfwCart, UfwCartNew)
        {
            this.currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            //WindowsIdentity a = WindowsIdentity.GetCurrent();
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
                    currentUser = null;
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

        protected override void Execute(RequestContext requestContext)
        {
            var x = 1;

            base.Execute(requestContext);
        }

        protected ApplicationUser UsuarioAtual {
            get { return currentUser; }
        } 

    }
}