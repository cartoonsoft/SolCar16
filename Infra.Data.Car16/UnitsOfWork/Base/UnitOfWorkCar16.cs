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
using System.Web.Configuration;
using Domain.Car16.enums;
using Domain.Car16.Interfaces.UnitOfWork;
using Infra.Data.Car16.Context;
using Infra.Data.Car16.Repositories.Base;

namespace Infra.Data.Car16.UnitsOfWork.Base
{
    public class UnitOfWorkCar16 : UnitOfWork, IUnitOfWorkCar16
    {
        const string LOG_NAME = "log_car16_InfraDataUnitOfWork";
        const string SOURCE = "CartoonSoft-Car16";
        private readonly InfraDataEventLogging _log;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWorkCar16(BaseDados baseDados, ContextMainCar16 context = null, InfraDataEventLogging log = null ) : base(context)
        {
            //
            string contextName = string.Empty;

            this.ContextMainCar16 = context;

            if (ContextMainCar16 == null)
            {
                switch (baseDados)
                {
                    case BaseDados.DesenvDezesseisNew:
                        contextName = "contextOraDevCartorioNew";
                        break;
                    case BaseDados.DesenvDezesseis:
                        contextName = "contextOraDevCartorio";
                        break;
                    case BaseDados.HomoloDezesseisNew:
                        contextName = "contextOraHomoloCartorioNew";
                        break;
                    case BaseDados.HomoloDezesseis:
                        contextName = "contextOraHomoloCartorio";
                        break;
                    case BaseDados.ProdDezesseisNew:
                        contextName = "contextOraProdCartorioNew";
                        break;
                    case BaseDados.ProdDezesseis:
                        contextName = "contextOraProdCartorio";
                        break;
                    default:
                        break;
                }

                this.ContextMainCar16 = new ContextMainCar16(contextName);
                base.ContextCore = this.ContextMainCar16;
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
                    if (_log != null)
                    {
                        //_log.Dispose();
                    }

                    if(this.ContextMainCar16 != null)
                    {
                        ContextMainCar16.Dispose();
                    }
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
        
        protected ContextMainCar16 ContextMainCar16 { get; private set; }

        public override int? Commit()
        {
            //fazer algo antes de salvar tudo

            return base.Commit();
            //fazer algo depoi de salvar tudo
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
