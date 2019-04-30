using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Car16.Entities.Cadastros
{
    public class DtoCadastroDeAto
    {
        public DtoPREIMO DtoPREIMO { get; set; }
        public DtoPESSOA DtoPESSOA { get; set; }
        public DtoArquivoModeloSimplificadoDocxList DtoArquivoModeloSimplificadoDocxList { get; set; }
        public long IdTipoAto { get; set; }
        public string Ato { get; set; }
        public long? IrParaFicha { get; set; }
    }
}
