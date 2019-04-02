using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using Domain.Car16.Interfaces.UnitOfWork;
using Infra.Data.Car16.UnitOfWorkCar16;
using System.Reflection;
using AppServices.Car16.Interfaces;
using AppServices.Car16.AppServices;
using Domain.Car16.enums;

namespace Infra.Cross.Ioc
{
    public class BootStrapperAppMin
    {

        public static Container ContainerAppMin { get; set; }

        public static void Register(Container container)
        {
            // Lifestyle.Transient => Uma instancia para cada solicitacao;
            // Lifestyle.Singleton => Uma instancia unica para a classe;
            // Lifestyle.Scoped => Uma instancia unica para o request;

            ContainerAppMin = container;

            // Infra Dados: Context and UnitofWork 
            //ContainerAppMin.Register<ContextMainCar16>(Lifestyle.Scoped);
            ContainerAppMin.Register<IUnitOfWorkCar16>(() => new UnitOfWorkCar16(BaseDados.DesenvDezesseis),  Lifestyle.Scoped);

            // AppServices 
            //ContainerAppMin.Register<IAppServicePais, AppServicePais>(Lifestyle.Scoped);



        }
    }
}
