using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Car16.Entities.Cadastros
{
    public class DtoCadastroDeAto
    {
        public DtoPREIMO PREIMO { get; set; }
        public DtoPESSOA PESSOA { get; set; }
        public DtoArquivoModeloSimplificadoDocxList ArquivoModelo { get; set; }
        public long IdTipoAto { get; set; }
        public string Ato { get; set; }
        public int IrParaFicha { get; set; }
        public bool IrParaVerso { get; set; }
        public float QuantidadeCentimetrosDaBorda { get; set; }
    }
}
