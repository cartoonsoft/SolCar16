using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartNew.Enumerations
{
    public enum TipoFicha
    {
        [Description("Indefinido")]
        Registro = 0,
        [Description("Frente")]
        Averbacao = 1,
        [Description("Verso")]
        AtoInicial = 2
    }
}
