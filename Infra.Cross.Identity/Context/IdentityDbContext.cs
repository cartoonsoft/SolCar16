using Infra.Cross.Identity.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Cross.Identity.Context
{
    public class IdentityDbContext: IdentityDbContext<ApplicationUser>, IDisposable
    {
        public IdentityDbContext() : base("contextOraUserIdentity", throwIfV1Schema: false)
        {
            
        }

        public static IdentityDbContext Create()
        {
            return new IdentityDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("DEZESSEIS_NEW");

            base.OnModelCreating(modelBuilder);
        }
    }
}
