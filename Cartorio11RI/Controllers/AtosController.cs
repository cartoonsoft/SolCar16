﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using System.Reflection;
using Domain.CartNew.Enumerations;
using Domain.CartNew.Entities;
using Cartorio11RI.Controllers.Base;
using Cartorio11RI.ViewModels;
using AppServCart11RI.AppServices;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using Domain.CartNew.Interfaces.UnitOfWork;
using Dto.CartNew.Entities.Cart_11RI;
using LibFunctions.Functions.IOAdmCartorio;

namespace Cartorio11RI.Controllers
{
    [Authorize]
    public class AtosController : CartorioBaseController
    {
        public AtosController(IUnitOfWorkDataBaseCartNew UfwCartNew = null) : base(UfwCartNew)
        {
            //

        }

        // GET: Ato
        public ActionResult Index(DateTime? DataIni = null, DateTime? DataFim = null)
        {
            bool FlagErro = false;
            IEnumerable<AtoListViewModel> listaAtoListViewModel = new List<AtoListViewModel>();

            if ((DataIni != null) && (DataFim != null))
            {
                if (DataIni > DataFim)
                {
                    ModelState.AddModelError(Guid.NewGuid().ToString() , "Data inicial deve ser menor o igual à data final!!");
                    FlagErro = true;
                }
            } else {
                if (DataIni == null)
                {
                    DataIni = DateTime.Today;
                    DataFim = DateTime.Today;
                } else {
                    if (DataFim == null)
                    {
                        DataFim = DataIni;
                    }
                }
            }

            if (!FlagErro)
            {
                using (AppServiceAtos appService = new AppServiceAtos(this.UfwCartNew))
                {
                    IEnumerable<DtoAtoList> listaDto = appService.ListarAtos((DateTime)DataIni, (DateTime)DataFim).Where(a => a.Ativo == true);
                    if (listaDto != null)
                    {
                        listaAtoListViewModel = Mapper.Map<IEnumerable<DtoAtoList>, IEnumerable<AtoListViewModel>>(listaDto);
                    }
                }
            }

            ViewBag.DataIni = DataIni;
            ViewBag.DataFim = DataFim;

            return View(listaAtoListViewModel);
        }

        #region | CADASTRO |
        public ActionResult Novo()
        {
            var dados = new AtoViewModel();

            return View(dados);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Novo(AtoViewModel modelo)
        {
            string filePath = Server.MapPath($"~/App_Data/Arquivos/AtosPendentes/{modelo.PREIMO.MATRI}_pendente.docx");
            bool respEscreverWord = false;
            Ato ato;
            try
            {
                //throw new Exception("Teste Ronaldo");

                if (modelo.Id == null)
                {
                    ViewBag.erro = "O Ato é obrigatório";
                    return View(nameof(Novo), modelo);
                }

                //Ajusta a string de ato
                //modelo.Id??0 = RemoveUltimaMarcacao("" /*modelo.Id.ToString()*/);  //todo: ronalod arrumar 

                if (ModelState.IsValid)
                {
                    //Representa o documento e o numero de pagina
                    DtoCadastroDeAto modeloDto = Mapper.Map<AtoViewModel, DtoCadastroDeAto>(modelo);
                    long? numSequenciaAto = null;

                    if (modelo.NumSequenciaAto == 0 && modelo.IdTipoAto != (int)TipoAtoEnum.AtoInicial)
                    {
                        numSequenciaAto = this.UfwCartNew.Repositories.RepositoryAto.GetNumSequenciaAto(Convert.ToInt64(modelo.PREIMO.MATRI));
                        numSequenciaAto = numSequenciaAto != null ? numSequenciaAto + 1 : 1;
                    }
                    else
                    {
                        numSequenciaAto = modelo.NumSequenciaAto;
                    }

                    //todo: ronaldo arrumar AppServiceCadastroDeAto
                    //using (var appService = new AppServiceCadastroDeAto(this.UnitOfWorkDataBaseCartNew))
                    //{
                    //    respEscreverWord = appService.EscreverAtoNoWord(modeloDto, filePath, Convert.ToInt64(numSequenciaAto));
                    //}

                    if (respEscreverWord)
                    {
                        // Gravar no banco o array de bytes
                        var arrayBytesNovo = System.IO.File.ReadAllBytes(filePath);

                        // Gravar o ato e buscar o selo e gravar o selo
                        ato = new Ato()
                        {
                            Ativo = true,
                            Bloqueado = false,
                            IdPrenotacao = modelo.PREIMO.SEQPRE,
                            IdTipoAto = modelo.IdTipoAto,
                            //NomeArquivo = $"{ modelo.PREIMO.MATRI }.docx",
                            Observacao = "Cadastro de teste",
                            NumMatricula = modelo.PREIMO.MATRI.ToString(),
                            IdUsuarioCadastro = this.UsuarioAtual.Id,
                            IdCtaAcessoSist = 1
                            //NumSequencia = Convert.ToInt64(numSequenciaAto)
                        };

                        this.UfwCartNew.Repositories.GenericRepository<Ato>().Add(ato);
                        this.UfwCartNew.SaveChanges();
                    }
                    else
                    {
                        //Teve algum erro ao escrever o documento no WORD
                        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                    }
                    //ViewBag.sucesso = "Ato cadastrado com sucesso!";
                    return RedirectToActionPermanent(nameof(Bloquear), new { ato.Id });
                }

                ViewBag.erro = "Erro ao cadastrar o ato!";

                return View(nameof(Novo), modelo);
            }
            catch (Exception ex)
            {
                TypeInfo t = this.GetType().GetTypeInfo();
                IOFunctions.GerarLogErro(t, ex);
                return RedirectToAction("InternalServerError", "Adm", new { excecao = ex });
            }
        }

        public ActionResult Bloquear(long? Id)
        {
            try
            {
                if (Id.HasValue)
                {
                    Ato Ato = this.UfwCartNew.Repositories.GenericRepository<Ato>().GetById(Id);
                    if (Ato == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                    }
                    else if (Ato.Bloqueado == true)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Não é possível bloquear um ato já bloqueado");
                    }
                    AtoListViewModel atoViewModel = new AtoListViewModel
                    {
                        Id = Ato.Id,
                        Ativo = Ato.Ativo,
                        Bloqueado = Ato.Bloqueado,
                        //NumSequencia = Ato.NumSequencia,
                        Codigo = "",
                        DataAlteracao = Ato.DataAlteracao,
                        DataCadastro = Ato.DataCadastro,
                        //NomeArquivo = Ato.NomeArquivo,
                        NumMatricula = Ato.NumMatricula,
                        IdPrenotacao = Ato.IdPrenotacao
                    };

                    return View(atoViewModel);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                TypeInfo t = this.GetType().GetTypeInfo();
                IOFunctions.GerarLogErro(t, ex);
                return RedirectToAction("InternalServerError", "Adm", new { excecao = ex });
            }
        }
        [HttpPost]
        public void BloquearAto(long NumMatricula, long IdAto)
        {
            using (var appService = new AppServiceAtos(this.UfwCartNew))
            {

                var resultado = appService.FinalizarAto(IdAto);
                if (resultado)
                {
                    this.UfwCartNew.SaveChanges();
                    //todo: ronaldo fazer 
                    //WordHelper.EscreverAtoPrincipal(Server.MapPath($"~/App_Data/Arquivos/AtosPendentes/{NumMatricula}_pendente.docx"), Server.MapPath($"~/App_Data/Arquivos/Atos/{NumMatricula}.docx"));
                    Response.StatusCode = 200;
                    Response.Status = "Ato Bloqueado com sucesso!";
                }
                else
                {
                    Response.StatusCode = 500;
                    Response.Status = "Erro ao atualizar o ato!";
                }
            }
        }
        #endregion

        #region | EDITAR |
        public ActionResult Editar(long? Id)
        {
            try
            {
                if (Id.HasValue)
                {
                    Ato Ato = this.UfwCartNew.Repositories.GenericRepository<Ato>().GetById(Id);
                    if (Ato == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                    }
                    else if (Ato.Bloqueado == true)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Não é possível editar um ato já bloqueado.");
                    }
                    AtoViewModel atoViewModel = new AtoViewModel
                    {
                        Id = Id,
                        PREIMO = new PREIMOViewModel()
                        {
                            SEQIMO = Convert.ToInt64(Ato.NumMatricula),
                            SEQPRE = Ato.IdPrenotacao
                        },
                        NumSequenciaAto  = Ato.NumSequenciaAto
                    };

                    return View(atoViewModel);

                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                TypeInfo t = this.GetType().GetTypeInfo();
                IOFunctions.GerarLogErro(t, ex);
                return RedirectToAction("InternalServerError", "Adm", new { excecao = ex });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(AtoViewModel modelo)
        {
            string filePath = Server.MapPath($"~/App_Data/Arquivos/AtosPendentes/{modelo.PREIMO.MATRI}_pendente.docx");
            bool respEscreverWord = false;
            try
            {
                if (modelo.Id == null)
                {
                    ViewBag.erro = "O Ato é obrigatório";
                    return View(nameof(Editar), modelo);
                }

                //todo: ronaldo arrumar ato editar
                //Ajusta a string de ato
                //modelo.Id = RemoveUltimaMarcacao(modelo.Id);

                if (ModelState.IsValid)
                {
                    //Representa o documento e o numero de pagina
                    DtoCadastroDeAto modeloDto = Mapper.Map<AtoViewModel, DtoCadastroDeAto>(modelo);
                    long? numSequenciaAto = null;

                    if (modelo.NumSequenciaAto == 0 && modelo.IdTipoAto != (int)TipoAtoEnum.AtoInicial)
                    {
                        numSequenciaAto = this.UfwCartNew.Repositories.RepositoryAto.GetNumSequenciaAto(Convert.ToInt64(modelo.PREIMO.MATRI));
                        numSequenciaAto = numSequenciaAto != null ? numSequenciaAto : 1;
                    }
                    else
                    {
                        numSequenciaAto = modelo.NumSequenciaAto;
                    }

                    //using (var appService = new AppServiceCadastroDeAto(this.UnitOfWorkDataBaseCartorio, this.UnitOfWorkDataBaseCartNew))
                    //{

                    //    respEscreverWord = appService.EscreverAtoNoWord(modeloDto, filePath, Convert.ToInt64(numSequenciaAto));
                    //}

                    if (respEscreverWord)
                    {
                        // Gravar no banco o array de bytes
                        var arrayBytesNovo = System.IO.File.ReadAllBytes(filePath);

                        // Gravar o ato e buscar o selo e gravar o selo
                        using (var appService = new AppServiceAtos(this.UfwCartNew))
                        {
                            var dtoEditar = Mapper.Map<AtoViewModel, DtoCadastroDeAto>(modelo);

                            //var resultado = appService.EditarAto(dtoEditar, this.UsuarioAtual.Id);

                            //if (resultado)
                            //{
                            //    this.UnitOfWorkDataBaseCartNew.SaveChanges();
                            //}
                            //else
                            //{
                            //    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                            //}
                        }
                    }
                    else
                    {
                        //Teve algum erro ao escrever o documento no WORD
                        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                    }
                    //ViewBag.sucesso = "Ato cadastrado com sucesso!";
                    return RedirectToActionPermanent(nameof(Bloquear), new { Id = modelo.Id });
                }

                ViewBag.erro = "Erro ao cadastrar o ato!";

                return View(nameof(Editar), modelo);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                throw;
            }
        }
        #endregion

        #region | VIEWS PARCIAIS |
        public PartialViewResult PartialDadosAdicionais()
        {
            return PartialView();
        }
        public PartialViewResult PartialDadosPessoas(string listaPessoas)
        {
            var dados = JsonConvert.DeserializeObject<List<DadosPessoaViewModel>>(listaPessoas);
            return PartialView(dados);
        }

        #endregion

        #region | JsonResults e .GET |

        /// <summary>
        /// Lista de Modelos (JSON)
        /// </summary>
        /// <returns>Lista de arquivos</returns>
        public JsonResult GetModelos()
        {
            using (var appService = new AppServiceModelosDocx(this.UfwCartNew))
            {
                var listaDtoArquivoModelosDocx = appService.ListarModeloSimplificado();
                var listaModelos = Mapper.Map<IEnumerable<DtoModeloDocxSimplificadoList>, IEnumerable<ModeloDocxSimplificadoViewModel>>(listaDtoArquivoModelosDocx);
                var jsonResult = JsonConvert.SerializeObject(listaModelos);

                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetDadosImovel(long? numeroMatricula = null, long? numeroPrenotacao = null)
        {
            string jsonResult;
            try
            {
                //var PREIMO = this.UfwCartNew.Repositories.RepositoryPREIMO.BuscaDadosImovel(numeroPrenotacao, numeroMatricula);
                //jsonResult = JsonConvert.SerializeObject(PREIMO);
            }
            catch (Exception)
            {
                jsonResult = "";
                Response.StatusCode = 500;
                Response.Status = "Erro ao serializar o objeto";
                //Cadastrar log de erro
            }
            Response.StatusCode = 200;
            return null; // Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Essa função retorna uma lista de pessoa por um id de prenotação
        /// </summary>
        /// <param name="numeroPrenotacao">Numero da prenotação</param>
        /// <returns>JSON</returns>
        public JsonResult GetPessoasPremo(long numeroPrenotacao)
        {
            var jsonResult = "";
            try
            {
                //using (AppServicePessoa appServicePessoa = new AppServicePessoa(this.UfwCart, this.UfwCartNew))
                //{
                //    jsonResult = JsonConvert.SerializeObject(appServicePessoa.GetPessoasPorPrenotacao(numeroPrenotacao));
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Response.StatusCode = 500;
                Response.Status = "Erro ao buscar os dados das pessoas";
            }
            Response.StatusCode = 200;
            return null; // Json(jsonResult, JsonRequestBehavior.AllowGet);

        }
        public long GetIdTipoAtoPeloModelo(long idModelo)
        {

            return this.UfwCartNew.Repositories.RepositoryModeloDocx.GetById(idModelo).IdTipoAto;

        }
        public bool ExisteAto(long numeroMatricula)
        {
            try
            {
                string filePath = Server.MapPath($"~/App_Data/Arquivos/Atos/{numeroMatricula}.docx");
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {

                }
                Response.StatusCode = 200;
                return true;
            }
            catch (FileNotFoundException ex)
            {
                Response.StatusCode = 200;
                Console.Write(ex);
                return false;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Console.Write(ex);
                return false;
            }
        }

        /// <summary>
        /// Pega o arquivo DOCX do ATO
        /// </summary>
        /// <param name="dadosPost">Dados do post</param>
        /// <returns>Download do arquivo DOCX</returns>
        public FileResult DownloadFile([Bind(Include = "Id")]long? Id)
        {
            string fileName = Id.ToString();
            string filePath = Server.MapPath($"~/App_Data/Arquivos/AtosPendentes/{Id}_pendente.docx");
            try
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                Response.StatusCode = 200;
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = 500;
                return null;
                throw;
            }
        }

        /// <summary>
        /// Pega o arquivo DOCX do ATO
        /// </summary>
        /// <param name="dadosPost">Dados do post</param>
        /// <returns>Download do arquivo DOCX</returns>
        public FileResult DownloadFileCompleto([Bind(Include = "Id")]long? Id)
        {
            string fileName = Id.ToString();
            string filePath = Server.MapPath($"~/App_Data/Arquivos/Atos/{Id}.docx");
            try
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                Response.StatusCode = 200;
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = 500;
                return null;
                throw;
            }
        }
        #endregion

        #region | Funcoes auxiliares |
        /// <summary>
        /// Retorna o numero de Ato do modelo
        /// </summary>
        /// <param name="modelo">Modelo</param>
        /// <returns>N° da Ato</returns>
        public static long GetNumeroAto(MatriculaAtoViewModel modelo)
        {
            return modelo.IdMatricula;
        }

        /// <summary>
        /// Remove a ultima marcação de espaço da string (\n)
        /// </summary>
        /// <param name="ato">ATO como String</param>
        /// <returns>Ato como string</returns>
        private static string RemoveUltimaMarcacao(string ato)
        {
            var atoString = ato.Substring(0, ato.Length - 1);
            atoString = atoString.Replace('\n', ' ').Replace("&nbsp;", "");
            return atoString;
        }

        /// <summary>
        /// Função que monta uma string HTML para mostrar na tela exatamente 
        /// oque esta escrito no documento
        /// </summary>
        /// <returns>string HTML</returns>
        public string UsaModeloParaAto([Bind(Include = "Id,IdMatricula,IdPrenotacao,listIdsPessoas,IdTipoAto")]DadosPostModelo DadosPostModelo)
        {

            using (var appServiceAto = new AppServiceAtos(this.UfwCartNew))
            {
                //appServiceAto.
            }

            return null;

            /*
            using (var appService = new AppServicePessoa(this.UnitOfWorkDataBaseCartorio, this.UnitOfWorkDataBaseCartNew))
            {
                DtoDadosImovel dadosImovel = appService.GetCamposModeloMatricula(DadosPostModelo.listIdsPessoas, DadosPostModelo.IdTipoAto, DadosPostModelo.IdPrenotacao, DadosPostModelo.IdMatricula);
                StringBuilder textoFormatado = new StringBuilder();

                string filePath = Server.MapPath($"~/App_Data/Arquivos/Modelos/{DadosPostModelo.Id}.docx");
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
                                                    Response.StatusCode = 500;
                                                    Response.StatusDescription = "Arquivo com campos corrompidos, verifique o modelo";
                                                    return Response.StatusDescription;
                                                }
                                            }
                                            //Buscar dado da pessoa aqui
                                            //resultadoQuery = "teste query";
                                            resultadoQuery = GetValorCampoModeloMatricula(dadosImovel, nomeCampo);

                                            //atualiza o texto formatado
                                            textoParagrafo.Append(resultadoQuery);
                                        }
                                        else if (paragrafo.Text[i] == '<')
                                        {
                                            i++;
                                            var tipoTag = string.Empty;
                                            while (paragrafo.Text[i] != '>')
                                            {
                                                tipoTag += paragrafo.Text[i].ToString().Trim();
                                                i++;
                                                if (i >= paragrafo.Text.Length || paragrafo.Text[i] == '<')
                                                {
                                                    Response.StatusCode = 500;
                                                    Response.StatusDescription = "Tags de repetição corrompidas, verifique o modelo";
                                                    return Response.StatusDescription;
                                                }
                                            }
                                            i++;
                                            if (tipoTag.Equals("outorgantes"))
                                            {
                                                i = Repetir(dadosImovel, paragrafo, textoParagrafo, i);
                                            }
                                            else if (tipoTag.Equals("outorgados"))
                                            {
                                                i = Repetir(dadosImovel, paragrafo, textoParagrafo, i, false);
                                            }
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
                    Response.StatusCode = 200;
                    return textoFormatado.ToString();
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    Response.StatusCode = 404;
                    Response.StatusDescription = "Modelo não encontrado na base de dados";
                    return Response.StatusDescription;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Response.StatusCode = 500;
                    Response.StatusDescription = "Ocorreu algum erro ao utilizar o modelo";
                    return Response.StatusDescription;
                }

            }
            */
        }

        /// <summary>
        /// Função que repete o texto e popula para a pessoa
        /// </summary>
        /// <param name="texto">texto que esta sendo repetido</param>
        /// <param name="pessoa">Pessoa</param>
        /// <returns></returns>
        private string PopularCamposDoTexto(string texto, DtoPessoaPesxPre pessoa)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i] == '[')
                {
                    i++;
                    string nomeCampo = string.Empty;
                    string resultadoQuery = string.Empty;
                    while (texto[i] != ']')
                    {
                        nomeCampo += texto[i].ToString().Trim();
                        i++;
                        if (i >= texto.Length || texto[i] == '[')
                        {
                            Response.StatusCode = 500;
                            Response.StatusDescription = "Arquivo com campos corrompidos, verifique o modelo";
                            return Response.StatusDescription;
                        }
                    }
                    //Buscar dado da pessoa aqui
                    //resultadoQuery = "teste query";
                    resultadoQuery = GetValorCampoPessoa(pessoa, nomeCampo);

                    //atualiza o texto formatado
                    stringBuilder.Append(resultadoQuery);
                }
                else
                {
                    //caso não seja um campo somente adiciona o caractere
                    stringBuilder.Append(texto[i].ToString());
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Função que pega o campo independente da pessoa
        /// </summary>
        /// <param name="pessoa">Pessoa</param>
        /// <param name="campoQuery">Campo procurado</param>
        /// <returns></returns>
        private string GetValorCampoPessoa(DtoPessoaPesxPre pessoa, string campoQuery)
        {
            string Campotmp = string.Empty;
            try
            {
                foreach (var Campo in pessoa.listaCamposValor)
                {
                    if (Campo.Campo.Equals(campoQuery))
                    {
                        Campotmp = Campo.Valor;
                    }
                }
                //Retorna o dados das pessoas
                return string.IsNullOrEmpty(Campotmp.Trim()) ? $"[{campoQuery}]" : Campotmp;

            }
            catch (Exception)
            {
                return "[NÃO ENCONTRADO]";
                throw;
            }
        }

        private string GetValorCampoModeloMatricula(DtoDadosImovel dtoDados, string campoQuery)
        {
            string Campotmp = string.Empty;
            bool CampoEncontrado = false;

            try
            {
                //PESQUISA DADOS IMÓVEL
                foreach (var item in dtoDados.listaCamposValor)
                {
                    if (item.Campo.Equals(campoQuery))
                    {
                        //Retorna o campo
                        Campotmp = item.Valor;
                        CampoEncontrado = true;
                    }
                }

                //PESQUISA DADOS PESSOA
                if (!CampoEncontrado)
                {
                    foreach (var pessoas in dtoDados.Pessoas)
                    {
                        foreach (var pessoa in pessoas.listaCamposValor)
                        {
                            if (pessoa.Campo.Equals(campoQuery))
                            {
                                Campotmp = pessoa.Valor;
                            }
                        }
                    }
                }

                //Retorna o dados das pessoas
                return string.IsNullOrEmpty(Campotmp.Trim()) ? $"[{campoQuery}]" : Campotmp;

            }
            catch (Exception)
            {
                return "[NÃO ENCONTRADO]";
                throw;
            }
        }
        #endregion
    }
}