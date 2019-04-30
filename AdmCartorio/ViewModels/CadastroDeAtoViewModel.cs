using Domain.Car16.Entities.Car16;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdmCartorio.ViewModels
{
    public class CadastroDeAtoViewModel
    {
        public PREIMO PREIMO { get; set; }
        public PESSOA Pessoa { get; set; }
        public ArquivoModeloSimplificadoViewModel ArquivoModelo { get; set; }
        public long IdTipoAto { get; set; }
        public string Ato { get; set; }
    }
}