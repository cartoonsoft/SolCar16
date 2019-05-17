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
        public List<DtoPESSOA> Pessoas { get; set; }
        public DtoArquivoModeloSimplificadoDocxList ArquivoModelo { get; set; }
        public string TipoPessoa { get; set; }
        public long IdTipoAto { get; set; }
        public string Ato { get; set; }
        public int IrParaFicha { get; set; }
        public bool IrParaVerso { get; set; }
        public bool ExisteNoSistema { get; set; }
        public float QuantidadeCentimetrosDaBorda { get; set; }
        public int NumSequencia { get; set; }
        public long? IdAto { get; set; }
    }
}
