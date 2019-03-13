using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities;
using Infra.Data.Car16.Context.Base;

namespace Infra.Data.Car16.Context
{
    public class ContextMainCar16: ContextOraBase
    {
        public ContextMainCar16(string connName) : base(connName)
        {
            //this.Database.
            //
        }
        public DbSet<Pais> PaisDb { get; set; }
        
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
