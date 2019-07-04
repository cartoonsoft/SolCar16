using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Cartorio.enums
{
    public enum TipoConferenciaAto
    {
        [Description("Conferir texto/informações")]
        Documentacao = 1,
        [Description("Conferir impressão")]
        AtoInicial = 2
    }
}
