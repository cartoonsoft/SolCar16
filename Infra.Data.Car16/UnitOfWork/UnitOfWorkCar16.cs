using Domain.Car16.Interfaces.UnitOfWork;
using Infra.Data.Car16.Context;
using Infra.Data.Car16.Repositories.Base;
using Infra.Data.Car16.UnitOfWorkCar16.Base;

namespace Infra.Data.Car16.UnitOfWorkCar16
{
    public class UnitOfWorkCar16 : UnitOfWork, IUnitOfWorkCar16
    {
        const string LOG_NAME = "log_car16_InfraDataUnitOfWork";
        const string SOURCE = "CartoonSoft-Car16";
        
        private readonly ContextMainCar16 _context;
        private readonly InfraDataEventLogging _log;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWorkCar16(ContextMainCar16 context, InfraDataEventLogging log = null ) : base(context)
        {
            //
            _context = context;
            _log = log;
            
        }

        public ContextMainCar16 Context {
            get { return _context; }
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
