using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GemboxLib.Base
{
    public enum LayoutPage
    {
        [Description("Pagina A4 (210x297 mm)")]
        A4 = 1,

        [Description("Pagina B5 (182x257mm)")]
        B5 = 2,

        [Description("Definido pelo documento lido")]
        DefinedByDoc = 3,

        [Description("Layout de página indefinida")]
        None = 4
    }

}