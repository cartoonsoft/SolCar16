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
                    NaturezaCampoArquivoModelo = new TipoAtoAPI()
                    {
                        Descricao = "Ato Inicial",
                        Id = 1,
                        IdAcessoSistema = 1
                    },
                    CamposArquivoModelo = new List<CampoArquivoModeloAPI>(){
                        new CampoArquivoModeloAPI(){
                            NomeCampo = "Pune",
                            IdTipoAto = 1,
                            PlaceHolder ="Pune",
                            IdAcessoSistema = 1,
                            Id = 1
                        },
                        new CampoArquivoModeloAPI(){
                            NomeCampo = "Mumbai",
                            IdTipoAto = 1,
                            PlaceHolder ="Mumbai",
                            Id = 2,
                            IdAcessoSistema = 1
                        }
                    }
                });

            return dados;
        }
    }
}
