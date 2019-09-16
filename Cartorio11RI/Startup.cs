using System;
using System.Threading.Tasks;
using Cartorio11RI.App_Start;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Cartorio11RI.Startup))]
namespace Cartorio11RI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            //autoMapper
            Cartorio11RI.App_Start.AutoMapper.AutoMapperConfigCartorio11RI.RegisterMappingsCartorio11RI();

            ConfigureAuth(app);

            //app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            //{
            //    AuthenticationType = "ApplicationCookie", 
            //    LoginPath = new PathString("")
            //});

        }
    }
}
