using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities.Car16;
using Domain.Car16.Entities.Car16New;
using Infra.Data.Car16.Context.Base;

namespace Infra.Data.Car16.Context
{
    /// <summary>
    /// context para Car16 New
    /// </summary>
    [DbConfigurationType(typeof(EntityFrameworkOracleConfiguration))]
    public class ContextMainCar16: ContextOraBase
    {
        /// <summary>
        /// Base de dados: dezesseis_new 
        /// </summary>
        public DbSet<Pais> Pais { get; set; }

        /// <summary>
        /// Base de dados: dezesseis
        /// </summary>
        /// <param name="connName"></param>
        DbSet<Matricula> DbMatricula { get; set; }

        public ContextMainCar16(string connName) : base(connName)
        {
            Database.SetInitializer<ContextMainCar16>(null);
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new ClienteConfig());
            //modelBuilder.Configurations.Add(new EnderecoConfig());
            //modelBuilder.Entity<Matricula>().ToTable("MATRICULAS");
            //modelBuilder.Ignore<Matricula>();

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Salva os Dados do Contexto, vc pode dar override
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
