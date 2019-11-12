using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartNew.Enumerations
{
    public enum TipoFolhaFicha
    {
        [Description("Indefinido")]
        Indefinido = 0,
        [Description("Frente")]
        Frente = 1,
        [Description("Verso")]
        Verso = 2
    }
}
