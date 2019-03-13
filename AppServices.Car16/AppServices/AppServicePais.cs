using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Car16.AppServices.Base;
using AppServices.Car16.Interfaces;
using Domain.Car16.DomainServices;
using Domain.Car16.Entities;
using Domain.Car16.Interfaces.DomainServices;
using Domain.Car16.Interfaces.UnitOfWork;
using Dto.Car16.Entities.Cadastros;

namespace AppServices.Car16.AppServices
{
    public class AppServicePais : AppServiceBase<DtoPaisModel, Pais>, IAppServicePais
    {
        private readonly IPaisDomainService  paisDs = null;

        public AppServicePais(IUnitOfWorkCar16 unitOfWork) : base(unitOfWork)
        {
            //
            //appDomainServices.Add(typeof(PaisDomainService), new PaisDomainService(unitOfWork));
            //paisDs = domainService<PaisDomainService>() as IPaisDomainService;

            //paisDs = this.domainService<IPaisDomainService>
            //this.appDomainServices<>(); //.Add(new PaisDomainService(null)); //.Add(new PaisDomainService()); // do .domainService<Pais>() as IPaisDomainService; 
            
        }

        public IEnumerable<DtoPaisModel> BuscarPorNome(string nome)
        {
            //domainService<Pais>().

            //var rep = AppServiceUnitOfWork().Repository<Pais>();
            return null;
        }
    }
}
