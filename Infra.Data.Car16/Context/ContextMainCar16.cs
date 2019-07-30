using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cart.Entities;
using Infra.Data.Cartorio.Context.Base;

namespace Infra.Data.Cartorio.Context
{
    /// <summary>
    /// context para Cartorio New
    /// </summary>
    [DbConfigurationType(typeof(EntityFrameworkOracleConfiguration))]
    public class ContextMainCartorio : ContextOraBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connName"></param>
        public ContextMainCartorio(string connName) : base(connName)
        {
            Database.SetInitializer<ContextMainCartorio>(null);
        }

        /// <summary>
        /// Base de dados: dezesseis_new 
        /// </summary>
        public DbSet<Matricula> DbMatricula { get; set; }
        public DbSet<PREIMO> DbPREIMO { get; set; }
        public DbSet<PESXPRE> DbPESXPRE { get; set; }
        public DbSet<PessoaCart> DbPESSOA { get; set; }
        public DbSet<PREMAD> DbPREMAD { get; set; }
        public DbSet<ACESSO> DbACESSO { get; set; }

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
