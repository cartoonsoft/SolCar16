using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Configuration;
using Domain.Car16.enums;
using Domain.Car16.Interfaces.Repositories;
using Domain.Car16.Interfaces.UnitOfWork;
using Infra.Data.Car16.Context;
using Infra.Data.Car16.Repositories;
using Infra.Data.Car16.Repositories.Base;
using Infra.Data.Car16.UnitsOfWork.Base;

namespace Infra.Data.Car16.UnitsOfWork
{
    public class UnitOfWorkCar16 : UnitOfWork, IUnitOfWorkCar16
    {
        const string LOG_NAME = "log_car16_InfraDataUnitOfWork";
        const string SOURCE = "CartoonSoft-Car16";
        private readonly InfraDataEventLogging _log;
        private readonly IRepositoriesFactoryCar16 _repositoriesCar16;

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

            _repositoriesCar16  = new RepositoriesFactoryCar16(ContextMainCar16);
            base.Repositories = _repositoriesCar16;
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

                    if (this._repositoriesCar16 != null)
                    {
                        this._repositoriesCar16.Dispose();
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
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion


        public ContextMainCar16 ContextMainCar16 { get; private set; }

        public new IRepositoriesFactoryCar16 Repositories
        {
            get { return _repositoriesCar16; }

        }

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
