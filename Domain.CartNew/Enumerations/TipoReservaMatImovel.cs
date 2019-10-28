using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartNew.Enumerations
{
    public enum TipoReservaMatImovel
    {
        [Description("Reservar")]
        Reservar = 1,
        [Description("Liberar")]
        Liberar = 2
    }
}
