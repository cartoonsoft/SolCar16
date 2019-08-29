using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cartorio11RI.ViewModels
{
    public class PREIMOViewModel
    {
        public long SEQIMO { get; set; }

        [Display(Name = "Num. prenotação")]
        public long SEQPRE { get; set; }

        public int? SUBD { get; set; }

        [StringLength(4)]
        public string TIPO { get; set; }

        [StringLength(4)]
        public string TITULO { get; set; }

        [Display(Name = "Endereço")]
        [StringLength(100)]
        public string ENDER { get; set; }

        [Display(Name = "Núm.")]
        [StringLength(20)]
        public string NUM { get; set; }

        [Display(Name = "Lote")]
        [StringLength(10)]
        public string LOTE { get; set; }

        [Display(Name = "Quadra")]
        [StringLength(10)]
        public string QUADRA { get; set; }

        [Display(Name = "Apt")]
        [StringLength(20)]
        public string APTO { get; set; }

        [Display(Name = "Bloco")]
        [StringLength(10)]
        public string BLOCO { get; set; }

        [Display(Name = "Edfício")]
        [StringLength(50)]
        public string EDIF { get; set; }

        [Display(Name = "Vaga")]
        [StringLength(20)]
        public string VAGA { get; set; }

        [Display(Name = "Outras infomações")]
        [StringLength(300)]
        public string OUTROS { get; set; }

        [Display(Name = "Núm. matrícula imóvel")]
        public int MATRI { get; set; }

        public int TRANS { get; set; }

        public int INSCR { get; set; }

        public int HIPO { get; set; }

        public int RD { get; set; }

        public long CONTRIB { get; set; }
    }
}