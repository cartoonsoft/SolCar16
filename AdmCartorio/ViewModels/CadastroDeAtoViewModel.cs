using Domain.Car16.Entities.Car16;
using Dto.Car16.Entities.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdmCartorio.ViewModels
{
    public class CadastroDeAtoViewModel
    {
        public PREIMOViewModel PREIMO { get; set; }
        public PESSOAViewModel Pessoa { get; set; }
        public ArquivoModeloSimplificadoViewModel ArquivoModelo { get; set; }
        public string TipoPessoa { get; set; }
        public long IdTipoAto { get; set; }
        public string Ato { get; set; }
        public int IrParaFicha { get; set; }
        public bool IrParaVerso { get; set; }
        public float QuantidadeCentimetrosDaBorda { get; set; }
        public int NumSequencia { get; set; }
    }
}