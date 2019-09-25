using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace GemboxLib.Base
{
    public class WordGemboxCartoonSoft: IDisposable
    {
        private LayoutPage layoutDoc = LayoutPage.None;

        public WordGemboxCartoonSoft()
        {
            //
        }

        public WordGemboxCartoonSoft(string filePathName)
        {
            if (!VerificarSeArquivoExiste(filePathName))
            {
                throw new IOException(string.Format("Documento : {0} não encontrado no servidor!", filePathName));
            }
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
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.
                disposedValue = true;
            }
        }

        // override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AppServiceBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public virtual void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        protected bool VerificarSeArquivoExiste(string filePathName)
        {
            bool existe = File.Exists(filePathName);
            return existe;
        }
        
        protected void VerificarLayoutPagina()
        {
            if (this.layoutDoc == LayoutPage.None)
            {
                throw new NullReferenceException("Layout de página indefinido");
            }
        }

        protected LayoutPage LayoutDocument
        {
            get { return layoutDoc; }
            set { layoutDoc = value; }
        }

    }
}