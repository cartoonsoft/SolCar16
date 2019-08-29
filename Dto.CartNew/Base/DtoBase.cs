using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Base
{
    public class DtoBase
    {
        public DtoBase()
        {
            this.resposta = false;
            this.msg = string.Empty;
        }

        public bool resposta { get; set; }

        public string msg { get; set; }
    }
}
