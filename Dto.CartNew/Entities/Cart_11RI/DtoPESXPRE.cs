using Dto.CartNew.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI
{
    public class DtoPESXPRE: DtoBase
    {
        public long SEQPES { get; set; }
        public long SEQPRE { get; set; }
        public string REL { get; set; }
        public long SEQINC { get; set; }
    }
}
