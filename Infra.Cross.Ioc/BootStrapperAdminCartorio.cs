using SimpleInjector;
using Domain.CartNew.Interfaces.UnitOfWork;

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
            //ContainerAppMin.Register<ContextMainCartorio>(Lifestyle.Scoped);

            // Infra Dados: UnitofWork 
            //ContainerAdmCartorio.Register<IUnitOfWorkDataBaseCartNew>(() => new UnitOfWorkDataBaseCartNew(BaseDados.DesenvDezesseisNew), Lifestyle.Scoped);
            //ContainerAdmCartorio.Register<IUnitOfWorkDataBaseCartorio>(() => new UnitOfWorkDataBaseCartorio(BaseDados.DesenvDezesseis), Lifestyle.Scoped);


            // AppServices 
            //ContainerAdmCartorio.Register<IAppServicePais>(() => new AppServicePais((IufwCart)container.GetInstance(typeof(IufwCart))), Lifestyle.Scoped);

        }
    }
}
