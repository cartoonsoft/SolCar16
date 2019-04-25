using Domain.Car16.Entities.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdmCartorioApi.Models
{
    public class NaturezaECamposArquivoModelo
    {
        public List<CampoArquivoModeloAPI> CamposArquivoModelo { get; set; }
        public TipoAtoAPI NaturezaCampoArquivoModelo { get; set; }
    }
}