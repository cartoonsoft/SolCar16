using SimpleInjector;
using Domain.CartNew.Interfaces.Repositories;
using Infra.Cross.Identity.Configuration;
using Infra.Data.CartNew.Repositories.Identity;
using Infra.Cross.Identity.Context;
using Microsoft.AspNet.Identity;
using Infra.Cross.Identity.Models;
using SimpleInjector.Integration.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Infra.Data.CartNew.UnitsOfWork.DbCartNew;
using Domain.CartNew.Interfaces.UnitOfWork;

namespace Infra.Cross.Ioc
{
    public class BootStrapperCartorio11RI
    {
        public static void RegisterServices(Container container)
        {
            container.Register<Identity.Context.IdentityDbContext>(Lifestyle.Scoped);
            container.Register<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new Identity.Context.IdentityDbContext()), Lifestyle.Scoped);
            container.Register<IRoleStore<IdentityRole, string>>(() => new RoleStore<IdentityRole>(), Lifestyle.Scoped);
            container.Register<ApplicationRoleManager>(Lifestyle.Scoped);
            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);
        }
    }
}
