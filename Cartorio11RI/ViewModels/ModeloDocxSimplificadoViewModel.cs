using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Cartorio11RI.ViewModels
{
    public class ModeloDocxSimplificadoViewModel
    {
        public ModeloDocxSimplificadoViewModel(long idCtaAcessoSist)
        {
            this.IdCtaAcessoSist = IdCtaAcessoSist;
        }

        public long? Id { get; set; }

        public long IdCtaAcessoSist { get; set; }

        public long IdTipoAto { get; set; }

        public string NomeModelo { get; set; }

        public string DescricaoTipoAto { get; set; }

    }
}