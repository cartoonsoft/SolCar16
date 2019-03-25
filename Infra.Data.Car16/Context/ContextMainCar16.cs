using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities;
using Domain.Car16.Entities.API;
using Infra.Data.Car16.Context.Base;

namespace Infra.Data.Car16.Context
{
    [DbConfigurationType(typeof(OraDbConfiguration))]
    public class ContextMainCar16: ContextOraBase
    {
        private readonly string _connName;
        public ContextMainCar16(string connName) : base(connName)
        {
            //
        }

        #region | DB SETS |
        public DbSet<Pais> PaisDb { get; set; }
        public DbSet<ArquivoModeloDocx> ArquivoModeloDocxDb { get; set; }
        public DbSet<LogArquivoModeloDocx> LogArquivoModeloDocxDb { get; set; }
        public DbSet<CampoArquivoModelo> CampoArquivoModeloDb { get; set; }
        public DbSet<TipoAtoAPI> NaturezaCampoArquivoModeloDb { get; set; }
        public DbSet<TipoAto> TipoAtoDb { get; set; }


        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new ClienteConfig());
            //modelBuilder.Configurations.Add(new EnderecoConfig());

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
