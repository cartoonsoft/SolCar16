using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.enums
{
    public enum TipoLogArquivoModeloDocx
    {
        [Description("Upload")] 
        Upload = 1,
        [Description("Download")]
        Download = 2
    }
}
