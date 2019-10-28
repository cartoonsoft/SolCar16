using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartNew.Enumerations
{
    public enum TipoMsgResposta
    {
        [Description("Undefined")]
        undefined = 0,
        [Description("Ok")]
        ok = 1,
        [Description("Confirm")]
        confirm = 2,
        [Description("Executed")]
        executed = 3,
        [Description("Warning")]
        warning = 4,
        [Description("Error")]
        error = 5
    }
}
