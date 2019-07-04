using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Oracle.ManagedDataAccess.Client;
using AdmCartorio.Models.Identity.Entities;
using Infra.Data.Cartorio.Context;

namespace AdmCartorio.Models.Identity.Context
{
    [DbConfigurationType(typeof(EntityFrameworkOracleConfiguration))]
    public class ApplicationDbContextIdentity : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContextIdentity(): base("contextOraDevUserIdentity", throwIfV1Schema: false)
        {
            //coloquei no web.config
            //DbConfiguration.SetConfiguration(EntityFrameworkOracleConfiguration.Instance);
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

            if (Database.Connection is OracleConnection)
            {
                modelBuilder.HasDefaultSchema("DEZESSEIS_NEW");
                modelBuilder.Properties<string>()
                    .Configure(p => p.HasColumnType("VARCHAR2"));

            }
            else if (this.Database.Connection is SqlConnection)
            {
                modelBuilder.Properties<string>()
                    .Configure(p => p.HasColumnType("varchar"));
                modelBuilder.HasDefaultSchema("dbo");
            }

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Client> Client { get; set; }

        public DbSet<Claims> Claims { get; set; }



    }
}