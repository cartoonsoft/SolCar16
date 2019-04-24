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
            modelBuilder.Entity<TB_MODELO_DOC>()
                .Property(e => e.ID_MODELO_DOC)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TB_MODELO_DOC>()
                .Property(e => e.ID_TP_ATO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TB_MODELO_DOC>()
                .Property(e => e.ID_CTA_ACESSO_SIST)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TB_MODELO_DOC>()
                .Property(e => e.ID_USR_CAD)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TB_MODELO_DOC>()
                .Property(e => e.ID_USR_ALTER)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TB_MODELO_DOC>()
                .Property(e => e.DESCRICAO)
                .IsUnicode(false);

            modelBuilder.Entity<TB_MODELO_DOC>()
                .Property(e => e.ARQUIVO)
                .IsUnicode(false);
        }
    }
}
