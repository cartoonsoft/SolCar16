using AdmCartorio.Controllers.Base;
using AdmCartorio.Models;
using AdmCartorio.ViewModels;
using AppServices.Car16.AppServices;
using AutoMapper;
using Domain.Car16.Entities.Diversas;
using Domain.Car16.Interfaces.UnitOfWork;
using Dto.Car16.Entities.Cadastros;
using Dto.Car16.Entities.Diversos;
using HtmlAgilityPack;
using LibFunctions.Functions;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Mvc;
using Xceed.Words.NET;

namespace AdmCartorio.Controllers
{
    public class AtoController : AdmCartorioBaseController
    { 
        #region | Construtores |
        public AtoController() : base(null, null)
        {
            //

        }

        public AtoController(IUnitOfWorkDataBaseCar16 unitOfWorkDataBaseCar16, IUnitOfWorkDataBaseCar16New unitOfWorkDataBseCar16New) : base(unitOfWorkDataBaseCar16, unitOfWorkDataBseCar16New)
        {
            //Criar instancia dos seus App services aqui
        }
        #endregion

        // GET: Ato
        public ActionResult Index()
        {
            //var dados = new MatriculaAtoViewModel();
            //using (var appService = new AppServiceArquivoModeloDocx(this.UnitOfWorkDataBseCar16New))
            //{
            //    IEnumerable<DtoArquivoModeloSimplificadoDocxList> listaDtoArquivoModelosDocx = appService.ListarArquivoModeloSimplificado();
            //    dados.ModelosSimplificadoViewModel = Mapper.Map<IEnumerable<DtoArquivoModeloSimplificadoDocxList>, IEnumerable<ArquivoModeloSimplificadoViewModel>>(listaDtoArquivoModelosDocx);
            //    dados.MatriculasViewModel = getMatriculaViewModel();
            //}
            return View();
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(MatriculaAtoViewModel modelo)
        {
            //Ronaldo

            return View();
        }

        #region | CADASTRO |
        public ActionResult Cadastrar()
        {
            var dados = new CadastroDeAtoViewModel();
            
            return View(dados);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Cadastrar(CadastroDeAtoViewModel modelo)
        {
            string filePath = Server.MapPath($"~/App_Data/Arquivos/{modelo.PREIMO.MATRI}.docx");
            bool respEscreverWord = false;
            try
            {

                if (modelo.Ato == null)
                {                  
                    ViewBag.erro = "O Ato é obrigatório";
                    return View(nameof(Cadastrar), modelo);
                }
                
                //Ajusta a string de ato
                modelo.Ato = RemoveUltimaMarcacao(modelo.Ato);

                if (ModelState.IsValid)
                {

                    //Representa o documento e o numero de pagina
                    DtoCadastroDeAto modeloDto = Mapper.Map<CadastroDeAtoViewModel, DtoCadastroDeAto>(modelo);
                    
                    using (var appService = new AppServiceCadastroDeAto(this.UnitOfWorkDataBseCar16New))
                    {
                        respEscreverWord = appService.EscreverAtoNoWord(modeloDto, filePath);
                    }
                    if (respEscreverWord)
                    {
                        // Gravar no banco o array de bytes
                        var arrayBytesNovo = System.IO.File.ReadAllBytes(filePath);
                        // Pegar a ultima "versão" do ato e somar

                        // Gravar o ato e buscar o selo e gravar o selo
                    }
                    else
                    {
                        //Teve algum erro ao escrever o documento no WORD
                        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                    }
                    ViewBag.sucesso = "Ato cadastrado com sucesso!";
                    return View(nameof(Cadastrar), modelo);
                }

                ViewBag.erro = "Erro ao cadastrar o ato!";

                return View(nameof(Cadastrar), modelo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                throw;
            }
        }
        #endregion

        #region | EDITAR |

        #endregion
       
        #region | VIEWS PARCIAIS |
        public PartialViewResult BuscaAto()
        {
            return PartialView();
        }
        public PartialViewResult BuscaModelo(int? idAto)
        {
            if (idAto.HasValue) { return PartialView(); }
            return PartialView(nameof(BuscaAto));
        }
        #endregion

        #region | JsonResults |
        /// <summary>
        /// Função que retorna os arquivos de modelo (JSON)
        /// </summary>
        /// <returns>Lista de arquivos</returns>
        public JsonResult GetModelos()
        {
            using (var appService = new AppServiceArquivoModeloDocx(this.UnitOfWorkDataBseCar16New))
            {
                var listaDtoArquivoModelosDocx = appService.ListarArquivoModeloSimplificado();
                var listaModelos = Mapper.Map<IEnumerable<DtoArquivoModeloSimplificadoDocxList>, IEnumerable<ArquivoModeloSimplificadoViewModel>>(listaDtoArquivoModelosDocx);
                var jsonResult = JsonConvert.SerializeObject(listaModelos);
                return Json(jsonResult, JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult GetDadosImovel(long? numeroMatricula = null, long? numeroPrenotacao = null)
        {
            using (var appService = new AppServicePREIMO(this.UnitOfWorkDataBseCar16))
            {
                var PREIMO = appService.BuscaDadosImovel(numeroPrenotacao,numeroMatricula);
                var jsonResult = JsonConvert.SerializeObject(PREIMO);
                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion


        /// <summary>
        /// Retorna o numero de Ato do modelo
        /// </summary>
        /// <param name="modelo">Modelo</param>
        /// <returns>N° da Ato</returns>
        public static long GetNumeroAto(MatriculaAtoViewModel modelo)
        {
            return modelo.MatriculaID;
        }


        /// <summary>
        /// Deixa o texto transparente do arquivo
        /// </summary>
        /// <param name="docX">Representa o documento</param>
        private static void SetTextColorTransparent(DocX docX)
        {
            var texto = docX.Paragraphs;
            foreach (var item in texto)
            {
                item.Color(Color.Transparent);
                item.UnderlineStyle(UnderlineStyle.none);
            }
        }


        /// <summary>
        /// Remove a ultima marcação de espaço da string (\n)
        /// </summary>
        /// <param name="ato">ATO como String</param>
        /// <returns>Ato como string</returns>
        private static string RemoveUltimaMarcacao(string ato)
        {
            var atoString = ato.Substring(0,ato.Length-1);
            return atoString;
        }

        /// <summary>
        /// Função que monta uma string HTML para mostrar na tela exatamente 
        /// oque esta escrito no documento
        /// </summary>
        /// <returns>string HTML</returns>
        public string UsaModeloParaAto([Bind(Include = "ModeloNome")]string ModeloNome)
        {
            StringBuilder textoFormatado = new StringBuilder();

            string filePath = Server.MapPath($"~/App_Data/Arquivos/{ModeloNome}.docx");
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    //Carrega o Modelo
                    using (DocX docX = DocX.Load(fileStream))
                    {
                        //Varre todos os paragrafos do Modelo
                        foreach (var paragrafo in docX.Paragraphs)
                        {
                            if (paragrafo.Text != "")
                            {
                                StringBuilder textoParagrafo = new StringBuilder();
                                for (int i = 0; i < paragrafo.Text.Length; i++)
                                {
                                    if (paragrafo.Text[i] == '[')
                                    {
                                        i++;
                                        string nomeCampo = string.Empty;
                                        string resultadoQuery = string.Empty;
                                        while (paragrafo.Text[i] != ']')
                                        {
                                            nomeCampo += paragrafo.Text[i].ToString().Trim();
                                            i++;
                                            if (i >= paragrafo.Text.Length || paragrafo.Text[i] == '[')
                                            {
                                                return "Arquivo com campos corrompidos, verifique o modelo";
                                            }
                                        }
                                        //Buscar dado da pessoa aqui
                                        resultadoQuery = "teste query";

                                        //atualiza o texto formatado
                                        textoParagrafo.Append(resultadoQuery);
                                    }
                                    else
                                    {
                                        //caso não seja um campo somente adiciona o caractere
                                        textoParagrafo.Append(paragrafo.Text[i].ToString());
                                    }

                                }
                                // Populando campo de retorno
                                textoFormatado.Append($"<p>{textoParagrafo}</p>");
                            }
                        }
                    }
                }
                return textoFormatado.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Ocorreu algum erro ao utilizar o modelo");
            }
        }
    }
}
