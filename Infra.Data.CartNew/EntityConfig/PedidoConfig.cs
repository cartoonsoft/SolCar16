using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.CartNew.EntityConfig
{
    /*
    public class PedidoConfig : EntityTypeConfiguration<Pedido>
    {
        public PedidoConfig()
        {
            HasKey(p => p.PedidoId);

            HasOptional(p => p.Pagamento)
                .WithRequired(p => p.Pedido);

            Ignore(p => p.ValidationResult);

            ToTable("Pedidos");
        }
    }
    */
}
