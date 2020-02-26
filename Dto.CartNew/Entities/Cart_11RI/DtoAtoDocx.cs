using Dto.CartNew.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Key]
        public long IdAto { get; set; }

        [Key]
        public long IdDocx { get; set; }

        public long IdCtaAcessoSist { get; set; }


        public short IndexParagrafo { get; set; }

        public string Texto { get; set; }

        public DtoDocx Docx { get; set; } // 1 ..1 em relacao docx
    }
}
