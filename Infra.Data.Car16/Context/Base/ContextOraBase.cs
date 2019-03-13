/*----------------------------------------------------------------------------
  _____            _                    _____        __ _   
/  __ \          | |                  /  ___|      / _| |  
| /  \/ __ _ _ __| |_ ___   ___  _ __ \ `--.  ___ | |_| |_ 
| |    / _` | '__| __/ _ \ / _ \| '_ \ `--. \/ _ \|  _| __|
| \__/\ (_| | |  | || (_) | (_) | | | /\__/ / (_) | | | |_ 
 \____/\__,_|_|   \__\___/ \___/|_| |_\____/ \___/|_|  \__|
Todos os direitos reservados ®                       
-----------------------------------------------------------------------------*/
using Domain.Core.Interfaces.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Car16.Context.Base
{
    /// <summary>
    /// Context para banco de dados oracle
    /// </summary>
    [DbConfigurationType(typeof(OraDbConfiguration))]
    public class ContextOraBase: DbContext, IContextCore
    {
        private readonly string _contexName;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public ContextOraBase(string contexName) : base(contexName)
        {
            _contexName = contexName;
        }

        public string ContextName
        {
            get { return _contexName; }
        }

        //public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            if (this.Database.Connection is OracleConnection)
            {
                modelBuilder.HasDefaultSchema("DEZESSEIS_NEW");
                modelBuilder.Properties<string>()
                    .Configure(p => p.HasColumnType("VARCHAR2"));
            }
            else if (this.Database.Connection is SqlConnection)
            {
                modelBuilder.HasDefaultSchema("dbo");
            }

            // General Custom Context Properties
            modelBuilder.Properties()
                .Where(p => p.Name == "Id")
                .Configure(p => p.IsKey());


            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Salva os Dados do Contexto, vc pode dar override
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            try
            {
                foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                    }

                    if (entry.State == EntityState.Modified)
                    {
                        entry.Property("DataCadastro").IsModified = false;
                    }
                }

                foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataAlteracao") != null))
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property("DataAlteracao").IsModified = false;
                    }

                    if (entry.State == EntityState.Modified)
                    {
                        entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
                    }
                }

            }
            catch (Exception)
            {
                
                //throw;
            }

            return base.SaveChanges();
        }

    }
}
