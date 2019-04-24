using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;


[assembly: OwinStartup(typeof(AdmCartorio.Startup))]
namespace AdmCartorio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //autoMapper
            AdmCartorio.App_Start.AutoMapper.AutoMapperConfigAdmCartorio.RegisterMappingsAdmCartorio();

            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            ConfigureAuth(app);
            
        }
    }
}
