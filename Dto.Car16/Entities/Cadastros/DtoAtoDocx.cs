using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Cartorio.Entities.Cadastros
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
        public long IdContaAcessoSistema { get; set; }
        public long IdAto { get; set; }
        public long IdDocx { get; set; }
        public short IndexParagrafo { get; set; }
        public string TextoHtml { get; set; }
        public DtoDocx Docx { get; set; } // 1 ..1 em relacao docx
    }
}
