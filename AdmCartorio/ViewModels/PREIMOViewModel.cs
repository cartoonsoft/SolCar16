using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdmCartorio.ViewModels
{
    public class PREIMOViewModel
    {
        public long SEQIMO { get; set; }

        public long SEQPRE { get; set; }

        public short? SUBD { get; set; }
        public string TIPO { get; set; }
        public string TITULO { get; set; }

        public string ENDER { get; set; }
        public string NUM { get; set; }
        public string LOTE { get; set; }
        public string QUADRA { get; set; }

        public string APTO { get; set; }
        public string BLOCO { get; set; }

        public string EDIF { get; set; }
        public string VAGA { get; set; }
        public string OUTROS { get; set; }
        public int MATRI { get; set; }

        public int TRANS { get; set; }

        public int INSCR { get; set; }

        public int HIPO { get; set; }

        public int RD { get; set; }
        public string CONTRIB { get; set; }
    }
}