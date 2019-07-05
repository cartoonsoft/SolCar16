using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using Domain.Cartorio.Interfaces.UnitOfWork;
using System.Reflection;
using AppServices.Cartorio.Interfaces;
using AppServices.Cartorio.AppServices;
using Domain.Cartorio.enums;
using Infra.Data.Cartorio.UnitsOfWork;
using Infra.Data.Cartorio.UnitsOfWork.DbCartorioNew;

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
            //ContainerAppMin.Register<ContextMainCartorio>(Lifestyle.Scoped);

            // Infra Dados: UnitofWork 
            ContainerAppMin.Register<IunitOfWorkCartoonSoft>(() => new UnitOfWorkDataBaseCartorioNew(BaseDados.DesenvDezesseis), Lifestyle.Scoped);

            // AppServices 
            //ContainerAppMin.Register<IAppServicePais>(() => new AppServicePais((IUnitOfWorkDataBaseCartorioNew)container.GetInstance(typeof(IUnitOfWorkDataBaseCartorioNew))), Lifestyle.Scoped);

        }
    }
}
