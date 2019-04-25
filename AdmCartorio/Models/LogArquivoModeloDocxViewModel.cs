using Domain.Car16.Enumeradores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdmCartorio.Models
{
    public class LogArquivoModeloDocxViewModel
    {
        public long Id { get; set; }

        public TipoLogArquivoModeloDocx TipoLogArquivoModeloDocx { get; set; }

        public long ArquivoID { get; set; }

        [Required]
        public string IP { get; set; }

        //[Required]
        public string NomeUsuario { get; set; }

        public DateTime DataHora { get; set; }
    }
}