using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI
{
    public class DtoAtoDocx
    {
        public DtoAtoDocx()
        {
            this.Docx = new DtoDocx();
        }

        public DtoAtoDocx(DtoDocx dtoDocx)
        {
            this.Docx = dtoDocx;
        }

        public long? Id { get; set; }
        public long IdCtaAcessoSist { get; set; }
        public long IdAto { get; set; }
        public long IdDocx { get; set; }
        public short IndexParagrafo { get; set; }
        public string TextoHtml { get; set; }
        public DtoDocx Docx { get; set; } // 1 ..1 em relacao docx
    }
}
