using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cartorio11RI.ViewModels
{
    public class MatriculaAtoViewModel
    {
        public long IdMatricula { get; set; }
        public string MatriculaEndereco { get; set; }
        public string MatriculaOutorgante { get; set; }
        public string ModeloNome { get; set; }
        public string ModeloTipoAto { get; set; }
        public long IdTipoAto { get; set; }
        public int NumeroPrenotacao { get; set; }
        public string NomeImovel { get; set; }
        //Pode ter mais de um CPF para o mesmo ato
        public string ModeloCPF { get; set; }
        public string Ato { get; set; }
        public int NumeroAtoSequencia { get; set; }
        public int IrParaFicha { get; set; }
        public float QuantidadeCentimetrosDaBorda { get; set; }
        public bool IrParaVerso { get; set; }

    }
}