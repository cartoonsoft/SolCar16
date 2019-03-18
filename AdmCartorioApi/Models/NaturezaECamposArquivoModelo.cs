using Domain.Car16.Entities.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdmCartorioApi.Models
{
    public class NaturezaECamposArquivoModelo
    {
        public List<CampoArquivoModelo> CamposArquivoModelo { get; set; }
        public NaturezaCampoArquivoModelo NaturezaCampoArquivoModelo { get; set; }
    }
}