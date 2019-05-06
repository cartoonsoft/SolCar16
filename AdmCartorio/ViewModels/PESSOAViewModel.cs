using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdmCartorio.ViewModels
{
    public class PESSOAViewModel
    {
        [Required(ErrorMessage ="Campo obrigatório")]
        public long SEQPES { get; set; }
        [Required(ErrorMessage ="Campo obrigatório")]
        public string NOM { get; set; }
        public string ENDER { get; set; }
        public string BAI { get; set; }
        public string CID { get; set; }
        public string UF { get; set; }
        public int? CEP { get; set; }
        public string TEL { get; set; }
        public byte? TIPODOC1 { get; set; }
        public string NRO1 { get; set; }
        public string TIPODOC2 { get; set; }
        public string NRO2 { get; set; }
    }
}