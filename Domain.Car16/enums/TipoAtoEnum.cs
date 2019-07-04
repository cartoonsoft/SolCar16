using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Cartorio.enums
{
    public enum TipoAtoEnum
    {
        [Description("Registro")]
        Registro = 1,
        [Description("Averbação")]
        Averbacao = 2,
        [Description("Ato Inicial")]
        AtoInicial = 3
    }
}
