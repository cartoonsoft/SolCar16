using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI.Diversos
{
    public class DtoDadosImovel
    {
        public DtoDadosImovel()
        {
            ListaCamposValor = new List<DtoCamposValor>();
        }

        public long IdPrenotacao { get; set; }

        public long IdMatricula { get; set; }

        public string DataAtualExtenso { get; set; }

        public long SEQIMO { get; set; }

        public long SEQPRE { get; set; }

        public int? SUBD { get; set; }

        [StringLength(4)]
        public string TIPO { get; set; }

        [StringLength(4)]
        public string TITULO { get; set; }

        [StringLength(100)]
        public string ENDER { get; set; }

        [StringLength(20)]
        public string NUM { get; set; }

        [StringLength(10)]
        public string LOTE { get; set; }

        [StringLength(10)]
        public string QUADRA { get; set; }

        [StringLength(20)]
        public string APTO { get; set; }

        [StringLength(10)]
        public string BLOCO { get; set; }

        [StringLength(50)]
        public string EDIF { get; set; }

        [StringLength(20)]
        public string VAGA { get; set; }

        [StringLength(300)]
        public string OUTROS { get; set; }

        public int MATRI { get; set; }

        public int TRANS { get; set; }

        public int INSCR { get; set; }

        public int HIPO { get; set; }

        public int RD { get; set; }

        public long CONTRIB { get; set; }

        public List<DtoCamposValor> ListaCamposValor { get; set; }
    }
}
