using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GemboxLib.Base
{
    public enum LoadOpitionsDocGemBox
    {
        [Description("Docx Defaut")]
        DocxDefaut = 1,

        [Description("Html")]
        Html = 2,

        [Description("Texto")]
        Txt = 3,

        [Description("Indefinido")]
        None = 4
    }

}