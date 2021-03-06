﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Cartorio.Interfaces;
using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.UnitOfWork;
using Dto.CartNew.Entities.TodosCart;

namespace AppServ.Core.AppServices
{
    public class AppServicePessoaNew : AppServiceCartorio<DtoPessoaCartNew, PessoaCartNew>, IAppServicePessoaNew
    {
        //private List<DtoPessoaPesxPre> listaDtoPessoaPesxPre = null;

        public AppServicePessoaNew(IUnitOfWorkDataBaseCartNew UfwCartNew, long IdCtaAcessoSist) : base(UfwCartNew, IdCtaAcessoSist)
        {
            //

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

        // override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AppServiceBase() {
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

    }
}