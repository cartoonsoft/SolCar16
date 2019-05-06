namespace AppBootStrapMin.ViewModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<TB_MODELO_DOC> TB_MODELO_DOC { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //
            //
            //
        }
    }
}
