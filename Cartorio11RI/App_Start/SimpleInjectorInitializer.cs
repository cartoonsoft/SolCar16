using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using Microsoft.Owin;
using Cartorio11RI.App_Start;
using WebActivatorEx;
using System.Reflection;
using System.Web.Mvc;
using Infra.Cross.Ioc;
using Domain.CartNew.Interfaces.UnitOfWork;
using Infra.Data.CartNew.UnitsOfWork.DbCartNew;
using Owin;

namespace Cartorio11RI.App_Start
{
    public static class SimpleInjectorInitializer
    {
        public static Container InitializeContainer()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            //container.Register<IAppBuilder>(Lifestyle.Singleton);

            container.Register<IUnitOfWorkDataBaseCartNew>(() => new UnitOfWorkDataBaseCartNew("contextOraCartNew"), Lifestyle.Scoped);

            // Chamada dos módulos do Simple Injector
            InitializeContainer(container);

            // Necessário para registrar o ambiente do Owin que é dependência do Identity
            // Feito fora da camada de IoC para não levar o System.Web para fora
            container.Register(() =>
            {
                if (HttpContext.Current != null && HttpContext.Current.Items["owin.Environment"] == null && container.IsVerifying)
                {
                    return new OwinContext().Authentication;
                }
                return HttpContext.Current.GetOwinContext().Authentication;

            }, Lifestyle.Scoped);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            return container;
        }

        private static void InitializeContainer(Container container)
        {
            BootStrapperCartorio11RI.RegisterServices(container);
        }
    }

}