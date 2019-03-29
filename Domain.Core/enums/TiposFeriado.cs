using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.enums
{
    public enum TiposFeriado
    {
        [Description("Feriado nacional")]
        FeriadoNaciona = 1,
        [Description("Feriado estadual")]
        FeriadoEstadual = 2,
        [Description("Feriado municipal")]
        FeriadoMunicipal = 3
    }
}
