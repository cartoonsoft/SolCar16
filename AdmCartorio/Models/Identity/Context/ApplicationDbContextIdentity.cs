using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AdmCartorio.Models.Identity.Context
{
    public class ApplicationDbContextIdentity : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContextIdentity(): base("ConnSqlServerLocalIdentity", throwIfV1Schema: false)
        {
        }

        //public DbSet<Client> Client { get; set; }

        //public DbSet<Claims> Claims { get; set; }

        public static ApplicationDbContextIdentity Create()
        {
            return new ApplicationDbContextIdentity();
        }
    }
}