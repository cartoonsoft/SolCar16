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
using System.Configuration;
using System.IO;
using System.Web;
using Domain.Core.Interfaces.UnitOfWork;
using Infra.Data.Core.Context;
using Infra.Data.Core.Repositories;

namespace Infra.Data.Cartorio.UnitsOfWork.Base
{
    public class UfwCart : UnitOfWork, IUfwCart
    {
        const string LOG_NAME = "log_Cartorio_InfraDataUnitOfWork";
        const string SOURCE = "CartoonSoft-Cartorio";

        private ContextOraBase _context;
        private readonly InfraDataEventLogging _log;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        public UfwCart(ContextOraBase context = null, InfraDataEventLogging log = null ) : base(context)
        {
            this._context = context;
            if (_context != null)
            {
                base.ContextCore = this._context;
            }

            _log = log;
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
            Dispose(true);
        }
        #endregion

        protected ContextOraBase Context
        {
            get { return _context; }
            set {
                _context = value;
                base.ContextCore = _context;
            }
        }

        public override int? SaveChanges()
        {
            //fazer algo antes de salvar 

            return base.SaveChanges();
            //fazer algo depoi de salvar 
        }

        /* ronaldo arrumar
        protected override void SaveLog(OracleException ex)
        {
            //base.SaveLog();

            if (_log != null)
            {

                foreach (var evento in ex.EntityValidationErrors)
                {
                    _log.WriteToEventLog(evento.Entry.Entity.GetType().Name + " state: " + evento.Entry.State, "error");
                    foreach (var validacao in evento.ValidationErrors)
                    {
                        _log.WriteToEventLog(validacao.PropertyName + " error: " + validacao.ErrorMessage, "error");
                    }
                }
            }
        }
        */
    }
}
