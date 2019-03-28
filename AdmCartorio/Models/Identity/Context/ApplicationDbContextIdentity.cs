using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AdmCartorio.Models.Identity.Context
{
    public class ApplicationDbContextIdentity : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContextIdentity(): base("ConnSqlServerLocalIdentity", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContextIdentity Create()
        {
            return new ApplicationDbContextIdentity();
        }
                
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder
                .Properties()
                .Where(p => p.PropertyType.Name == "String")
                .Configure(p => p.HasMaxLength(200));
            //string banco = this.Database.Connection.GetType().Name;

            if (false /*this.Database.Connection is OracleConnection*/)
            {
                modelBuilder.HasDefaultSchema("DEZESSEIS_NEW");
                modelBuilder.Properties<string>()
                    .Configure(p => p.HasColumnType("VARCHAR2"));
            }
            else if (this.Database.Connection is SqlConnection)
            {
                modelBuilder.Properties<string>()
                    .Configure(p => p.HasColumnType("VARCHAR"));
                modelBuilder.HasDefaultSchema("dbo");
            }

            base.OnModelCreating(modelBuilder);
        }

    }
}