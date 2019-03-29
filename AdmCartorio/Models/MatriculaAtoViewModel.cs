using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdmCartorio.Models
{
    public class MatriculaAtoViewModel
    {
        public long MatriculaID { get; set; }
        public string MatriculaEndereco { get; set; }
        public string MatriculaOutorgante { get; set; }
        public string ModeloNome { get; set; }
        public string ModeloTipoAto { get; set; }
        //Pode ter mais de um CPF para o mesmo ato
        public string ModeloCPF { get; set; }
        public string Ato { get; set; }

        public virtual List<MatriculaViewModel> MatriculasViewModel { get; set; }
        public virtual List<ArquivoModeloSimplificadoViewModel> ModelosSimplificadoViewModel { get; set; }
    }
}