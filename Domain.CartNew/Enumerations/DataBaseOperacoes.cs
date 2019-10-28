using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartNew.Enumerations
{
    public enum DataBaseOperacoes
    {
        [Description("Undefined")]
        undefined = 0,
        [Description("Insert")]
        insert = 1,
        [Description("Update")]
        update = 2,
        [Description("Delete")]
        delete = 3,
        [Description("Execute procedure")]
        execute = 4
    }
}
