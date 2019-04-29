using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.enums
{
    public enum TipoAto
    {
        [Description("Registro")]
        Registro,
        [Description("Averbação")]
        Averbacao,
        [Description("Ato Inicial")]
        AtoInicial
    }
}
