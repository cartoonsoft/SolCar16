using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cartorio.Entities.Cartorio;
using Domain.Cartorio.Entities.CartorioNew;
using Infra.Data.Cartorio.Context.Base;

namespace Infra.Data.Cartorio.Context
{
    /// <summary>
    /// context para Cartorio New
    /// </summary>
    [DbConfigurationType(typeof(EntityFrameworkOracleConfiguration))]
    public class ContextMainCartorioNew : ContextOraBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connName"></param>
        public ContextMainCartorioNew(string connName) : base(connName)
        {
            Database.SetInitializer<ContextMainCartorioNew>(null);
        }

        /// <summary>
        /// Base de dados: dezesseis_new 
        /// </summary>
        public DbSet<Pais> DbPais { get; set; }
        public DbSet<Uf> DbUf { get; set; }
        public DbSet<Municipio> DbMunicipio { get; set; }

        public DbSet<ArquivoModeloDocx> DbArquivoModeloDocx { get; set; }
        public DbSet<CamposArquivoModeloDocx> DbCamposArquivoModeloDocx { get; set; }
        public DbSet<LogArquivoModeloDocx> DbLogArquivoModeloDocx { get; set;}
        public DbSet<TipoAto> DbTipoAto { get; set; }
        public DbSet<Ato> DbAto { get; set; }

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
