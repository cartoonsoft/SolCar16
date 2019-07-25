using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities.Car16New;
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
        /// 
        public DbSet<CtaAcessoSist> DbCtaAcessoSist { get; set; }
        public DbSet<TipoCfgCtaAcessoSist> DbTipoCfgCtaAcessoSist { get; set; }
        public DbSet<CfgCtaAcessoSist> DbCfgCtaAcessoSist { get; set; }

        public DbSet<Status> DbStatus { get; set; }
        public DbSet<StatusValor> DbStatusValor { get; set; }
        public DbSet<StatusValorAnt> DbStatusValorAnt { get; set; }
        public DbSet<StatusValorPos> DbStatusValorPos { get; set; }

        public DbSet<Pais> DbPais { get; set; }
        public DbSet<Uf> DbUf { get; set; }
        public DbSet<Municipio> DbMunicipio { get; set; }

        public DbSet<ArquivoModeloDocx> DbArquivoModeloDocx { get; set; }
        public DbSet<CamposArquivoModeloDocx> DbCamposArquivoModeloDocx { get; set; }
        public DbSet<LogArquivoModeloDocx> DbLogArquivoModeloDocx { get; set;}
        public DbSet<TipoAto> DbTipoAto { get; set; }
        public DbSet<Ato> DbAto { get; set; }

        public DbSet<TipoMenu> DbTipoMenu { get; set; }
        public DbSet<Menu> DbMenuo { get; set; }
        public DbSet<Acao> DbAcao { get; set; }
        public DbSet<UsuarioAcao> DbUsuarioAcao { get; set; }

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
