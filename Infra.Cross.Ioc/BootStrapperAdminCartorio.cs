using SimpleInjector;
using Domain.Car16.Interfaces.UnitOfWork;
using Domain.Car16.enums;
using Infra.Data.Car16.UnitsOfWork.DbCar16New;
using Infra.Data.Car16.UnitsOfWork.DbCar16;

namespace Infra.Cross.Ioc
{
    public class BootStrapperAdminCartorio
    {

        public static Container ContainerAdmCartorio { get; set; }

        public static void Register(Container container)
        {
            // Lifestyle.Transient => Uma instancia para cada solicitacao;
            // Lifestyle.Singleton => Uma instancia unica para a classe;
            // Lifestyle.Scoped => Uma instancia unica para o request;

            ContainerAdmCartorio = container;

            // Infra Dados: Context and UnitofWork 
            //ContainerAppMin.Register<ContextMainCar16>(Lifestyle.Scoped);

            // Infra Dados: UnitofWork 
            ContainerAdmCartorio.Register<IUnitOfWorkDataBaseCar16New>(() => new UnitOfWorkDataBaseCar16New(BaseDados.DesenvDezesseisNew), Lifestyle.Scoped);
            ContainerAdmCartorio.Register<IUnitOfWorkDataBaseCar16>(() => new UnitOfWorkDataBaseCar16(BaseDados.DesenvDezesseis), Lifestyle.Scoped);


            // AppServices 
            //ContainerAdmCartorio.Register<IAppServicePais>(() => new AppServicePais((IUnitOfWorkCar16)container.GetInstance(typeof(IUnitOfWorkCar16))), Lifestyle.Scoped);

        }
    }
}
