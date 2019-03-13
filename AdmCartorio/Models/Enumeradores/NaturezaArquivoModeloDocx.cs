using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AdmCartorio.Models.Enumeradores
{
    public enum NaturezaArquivoModeloDocx
    {
        [Description("Imóveis")]
        Imoveis = 1,
        [Description("Civíl")]
        Civil = 2
    }
}