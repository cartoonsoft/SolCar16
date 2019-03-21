using Domain.Car16.Interfaces.Repositories;
using Domain.Car16.Interfaces.UnitOfWork;
using Infra.Data.Car16.Context;
using Infra.Data.Car16.enuns;
using Infra.Data.Car16.Repositories;
using Infra.Data.Car16.Repositories.Base;
using Infra.Data.Car16.UnitOfWorkCar16.Base;
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Configuration;
using static System.Net.WebRequestMethods;

namespace Infra.Data.Car16.UnitOfWorkCar16
{
    public class UnitOfWorkCar16 : UnitOfWork, IUnitOfWorkCar16
    {
        const string LOG_NAME = "log_car16_InfraDataUnitOfWork";
        const string SOURCE = "CartoonSoft-Car16";
        
        private readonly ContextMainCar16 _contextCar16;
        private readonly InfraDataEventLogging _log;
        private readonly IRepositoriesCar16 _repositoriesCar16;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWorkCar16(BaseDados baseDados, ContextMainCar16 context = null, InfraDataEventLogging log = null ) : base(context)
        {
            //
            string contextName = string.Empty;

            this._contextCar16 = context;

            if (_contextCar16 == null)
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

                _contextCar16 = new ContextMainCar16(contextName); 
            }

            _repositoriesCar16 = new RepositoriesCar16(_contextCar16);

            _log = log;
            
        }

        public IRepositoriesCar16 repositories
        {
            get { return _repositoriesCar16; }
        }

        public ContextMainCar16 GetContextMainCar16
        {
            get { return _contextCar16; }
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
