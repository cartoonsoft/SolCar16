using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartNew.Enumerations
{
    public enum TipoLogModeloDocx
    {
        [Description("Upload")] 
        Upload = 1,
        [Description("Download")]
        Download = 2
    }
}
