using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdmExemploCompleto.Startup))]
namespace AdmExemploCompleto
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
