using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Car16.EntityConfig
{
    /*
    public class PedidoItemConfig : EntityTypeConfiguration<PedidoItem>
    {
        public PedidoItemConfig()
        {
            HasKey(p => p.PedidoItemId);

            Property(p => p.PedidoId)
                .IsRequired();

            HasRequired(p => p.Pedido)
                .WithMany(p => p.PedidoItems)
                .HasForeignKey(p => p.PedidoId);

            ToTable("PedidoItems");
        }
    }
    */
}
