using AdmCartorioApi.Models;
using Domain.Car16.Entities.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace AdmCartorioApi.Controllers
{

    public class CamposArquivoModeloController : ApiController
    {


        // GET: CamposArquivoModelo
        public IEnumerable<NaturezaECamposArquivoModelo> Get()
        {
            List<NaturezaECamposArquivoModelo> dados = new List<NaturezaECamposArquivoModelo>();

            dados.Add(
                new NaturezaECamposArquivoModelo(){
                    NaturezaCampoArquivoModelo = new NaturezaCampoArquivoModelo()
                    {
                        Descricao = "Imoveis",
                        Id = 1,
                        IdEmpresaLogada = 1
                    },
                    CamposArquivoModelo = new List<CampoArquivoModelo>(){
                        new CampoArquivoModelo(){
                            Nome = "Pune",
                            IdNaturezaDocumento = 1,
                            PlaceHolder ="Pune",
                            IdEmpresaLogada = 1,
                            Id = 1
                        },
                        new CampoArquivoModelo(){
                            Nome = "Mumbai",
                            IdNaturezaDocumento = 1,
                            PlaceHolder ="Mumbai",
                            Id = 2,
                            IdEmpresaLogada = 1
                        }
                    }
                });

            return dados;
        }
    }
}
