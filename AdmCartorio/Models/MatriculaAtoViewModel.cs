using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdmCartorio.Models
{
    public class MatriculaAtoViewModel
    {
        public long MatriculaID { get; set; }
        public string MatriculaEndereco { get; set; }
        public string MatriculaProprietarioAtual { get; set; }
        public string ModeloNome { get; set; }
        public string ModeloTipoAto { get; set; }
        public string ModeloCPF { get; set; }

        public virtual List<MatriculaViewModel> MatriculasViewModel { get; set; }
        public virtual List<ArquivoModeloSimplificadoViewModel> ModelosSimplificadoViewModel { get; set; }
    }
}