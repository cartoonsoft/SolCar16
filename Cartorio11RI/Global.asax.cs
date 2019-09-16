using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Cartorio11RI.App_Start;

namespace Cartorio11RI
{

    public class MvcApplication : HttpApplication
    {
        public const long IdCtaAcessoSist = 1;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //simple injector
            SimpleInjectorInitializer.InitializeContainer();
        }
    }
}