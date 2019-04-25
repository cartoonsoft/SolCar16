using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdmCartorio.Models
{
    public class MatriculaViewModel
    {
        public long MatriculaId { get; set; }
        public string NomeImovel { get; set; }
        public string EnderecoImovel { get; set; }
        public string NomeProprietarioAtual { get; set; }

    }
}