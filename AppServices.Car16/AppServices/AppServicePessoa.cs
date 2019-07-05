using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cartorio.Entities.CartorioNew;
using Domain.Cartorio.Interfaces.UnitOfWork;
using Dto.Cartorio.Entities.Diversos;
using Dto.Cartorio.Entities.Cadastros;
using AppServices.Cartorio.AppServices.Base;
using AppServices.Cartorio.Interfaces;

namespace AppServices.Cartorio.AppServices
{
    public class AppServicePessoa : AppServiceCartorioNew<DtoPessoaCartorioNew, PessoaCartorioNew>, IAppServicePessoa
    {
        private readonly IUnitOfWorkDataBaseCartorio _ufwCart;
        private readonly IUnitOfWorkDataBaseCartorioNew _ufwCartNew;
        //private List<DtoPessoaPesxPre> listaDtoPessoaPesxPre = null;

        public AppServicePessoa(IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartorioNew UfwCartNew): base(UfwCart, UfwCartNew)
        {
            //
            _ufwCart = UfwCart;
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

        public IEnumerable<DtoPessoaPesxPre> GetPessoasPorPrenotacao(long IdPrenotacao)
        {
            throw new NotImplementedException();
        }


    }
}
