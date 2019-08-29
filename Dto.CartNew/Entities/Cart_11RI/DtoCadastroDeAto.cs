using Dto.CartNew.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI
{
    public class DtoCadastroDeAto: DtoBase
    {
        public DtoPREIMO PREIMO { get; set; }
        public List<DtoPessoaCartorio> Pessoas { get; set; }
        public DtoModeloDocxSimplificadoList ArquivoModelo { get; set; }
        public string TipoPessoa { get; set; }
        public long IdTipoAto { get; set; }
        public string Ato { get; set; }
        public int IrParaFicha { get; set; }
        public bool IrParaVerso { get; set; }
        public bool ExisteNoSistema { get; set; }
        public float QuantidadeCentimetrosDaBorda { get; set; }
        public int NumSequencia { get; set; }
        public long? IdAto { get; set; }
        public string Observacao { get; set; }
    }
}
