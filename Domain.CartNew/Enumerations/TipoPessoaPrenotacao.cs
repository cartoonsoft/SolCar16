using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartNew.Enumerations
{
    public enum TipoPessoaPrenotacao
    {
        [Description("Indefinido")]
        indefinido = 0,
        [Description("Outorgado")]
        outorgado = 1,
        [Description("Outorgante")]
        outorgante = 2
    }
}
