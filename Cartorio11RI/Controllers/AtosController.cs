﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using AutoMapper;
using Newtonsoft.Json;
using System.Reflection;
using Microsoft.AspNet.Identity.Owin;
using GemBox.Document;
using Domain.CartNew.Entities;
using Domain.CartNew.Entities.Diversos;
using Domain.CartNew.Enumerations;
using Domain.CartNew.Interfaces.UnitOfWork;
using Infra.Cross.Identity.Models;
using Infra.Cross.Identity.Configuration;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using Dto.CartNew.Base;
using Dto.CartNew.Entities.Cart_11RI;
using AppServCart11RI.AppServices;
using Cartorio11RI.Controllers.Base;
using Cartorio11RI.ViewModels;

namespace Cartorio11RI.Controllers
{
    [Authorize]
    public class AtosController : CartorioBaseController
    {
        public AtosController(IUnitOfWorkDataBaseCartNew UfwCartNew = null) : base(UfwCartNew)
        {
            //
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        #region privates methods
        private DtoExecProc InsertOrUpdateAto(DtoAto ato)
        {
            DtoExecProc execProc = new DtoExecProc();

            try
            {
                using (var appService = new AppServiceAtos(this.UfwCartNew, this.IdCtaAcessoSist, null))
                {
                    execProc = appService.InsertOrUpdateAto(ato, this.UsuarioAtual);
                }
            }
            catch (Exception ex)
            {
                execProc.TipoMsg = TipoMsgResposta.error;
                execProc.Msg = string.Format("{0}.{1} [{2}]", this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }

            return execProc;
        }

        private List<AtoListViewModel> GetListAtos(DateTime? DataIni = null, DateTime? DataFim = null)
        {
            bool FlagErro = false;
            List<AtoListViewModel> listaAtoViewModel = new List<AtoListViewModel>();

            if ((DataIni != null) && (DataFim != null))
            {
                if (DataIni > DataFim)
                {
                    ModelState.AddModelError(Guid.NewGuid().ToString(), "Data inicial deve ser menor o igual à data final!!");
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
                using (AppServiceAtos appService = new AppServiceAtos(this.UfwCartNew, this.IdCtaAcessoSist))
                {
                    ViewBag.StatusEditaveis = appService.StatusEditaveis;
                    ViewBag.StatusCamposReadOnly = appService.StatusCamposReadOnly;

                    var lista = appService.GetListAtosPeriodo((DateTime)DataIni, (DateTime)DataFim).Where(a => a.Ativo == true);
                    listaAtoViewModel = Mapper.Map<IEnumerable<DtoAto>, IEnumerable<AtoListViewModel>>(lista).ToList();
                }
            }

            ViewBag.DataIni = DataIni;
            ViewBag.DataFim = DataFim;

            return listaAtoViewModel;
        }

        #endregion

        // GET: Ato
        public ActionResult IndexAto(DateTime? DataIni = null, DateTime? DataFim = null)
        {
            List<AtoListViewModel> listaAtoViewModel = new List<AtoListViewModel>();
            listaAtoViewModel = this.GetListAtos(DataIni, DataFim);

            return View(listaAtoViewModel);
        }
        
        public JsonResult IndexAtoAjax(DateTime? DataIni = null, DateTime? DataFim = null)
        {
            bool resp = false;
            string mesage = string.Empty;
            List<AtoListViewModel> listaAtoViewModel = new List<AtoListViewModel>();

            try
            {
                listaAtoViewModel = this.GetListAtos(DataIni, DataFim);
                resp = true;
                mesage = "Dados retornados con sucesso";
            }
            catch (Exception ex)
            {
                mesage = "Falha ao obter dados! " + "[" + ex.Message + "]";
            }

            var resultado = new
            {
                resposta = resp,
                msg = mesage,
                ListaAtoViewModel = listaAtoViewModel
            };

            return Json(resultado);
        }

        #region |NovoAto|
        public ActionResult NovoAto()
        {
            var dados = new AtoViewModel();
            dados.IdCtaAcessoSist = this.IdCtaAcessoSist;
            dados.IdLivro = 1;
            dados.FolhaFicha = TipoFolhaFicha.Indefinido;
            dados.Salvo = false;
            dados.Ativo = true;

            //dados.Pessoas.Add(new PESSOAViewModel { 
            //    IdPessoa = 1,
            //    TipoPessoa = "Outorgante",
            //    NOM = "João de Teste",
            //    ENDER = "Rua XYZ, Nro. 123 Bairro Centro do Mundo"
            //});

            try
            {
                List<Livro> listaLivro = this.UfwCartNew.Repositories.GenericRepository<Livro>().Get().ToList();
                ViewBag.listaLivro = new SelectList(listaLivro, "Id", "Descricao");

                //povoar tree view
                List<TipoAtoList> listaTipoAto = this.UfwCartNew.Repositories.RepositoryTipoAto.GetListTipoAtos(null).ToList();
                ViewBag.listaTipoAto = listaTipoAto;

                ViewBag.listaModelosDocx = new SelectList(
                    new[] { new { IdModeloDoc = "0", NomeModelo = "Selecione um modelo" } },
                    "IdModeloDoc",
                    "NomeModelo"
                );
            }
            catch (Exception ex)
            {
                string msg = string.Format("Falha em: {0}.{1} [{2}{3}]", GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message, (ex.InnerException != null) ? "=>" + ex.InnerException.Message : "");
                TempData["excecaoGerada"] = ex;
                return RedirectToAction("InternalServerError", "Adm", new { descricao = msg });
            }

            return View(dados);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult NovoAto(AtoViewModel atoView)
        {
            DtoExecProc execProc = new DtoExecProc();
            DtoAto ato = new DtoAto();

            try
            {
                List<Livro> listaLivro = this.UfwCartNew.Repositories.GenericRepository<Livro>().Get().ToList();
                ViewBag.listaLivro = new SelectList(listaLivro, "Id", "Descricao");

                //povoar tree view
                List<TipoAtoList> listaTipoAto = this.UfwCartNew.Repositories.RepositoryTipoAto.GetListTipoAtos(null).ToList();
                ViewBag.listaTipoAto = listaTipoAto;

                ViewBag.listaModelosDocx = new SelectList(
                    new[] { new { IdModeloDoc = "0", NomeModelo = "Selecione um modelo" } },
                    "IdModeloDoc",
                    "NomeModelo"
                );

                if (ModelState.IsValid)
                {
                    execProc = this.InsertOrUpdateAto(ato);
                    atoView.Salvo = execProc.Resposta;

                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Falha em: {0}.{1} [{2}{3}]", GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message, (ex.InnerException != null) ? "=>" + ex.InnerException.Message : "");
                TempData["excecaoGerada"] = ex;
                return RedirectToAction("InternalServerError", "Adm", new { descricao = msg });
            }

            return View(atoView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public JsonResult InsertOrUpdateAtoAjax(AtoViewModel atoView)
        {
            bool resp = false;
            string msg = string.Empty;

            DtoExecProc execProc = new DtoExecProc();
            DtoAto ato = new DtoAto();
            ato = Mapper.Map<AtoViewModel, DtoAto>(atoView);

            try
            {
                execProc = this.InsertOrUpdateAto(ato);
                resp = execProc.Resposta;
                msg = execProc.Msg;
            }
            catch (Exception ex)
            {
                msg = string.Format("{0}.{1} [{2}]", this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }

            var resultado = new
            {
                resposta = resp,
                msg = msg,
                execute = execProc
            };

            return Json(resultado);
        }
        #endregion

        #region |EditarAto|
        public ActionResult EditarAto(long? Id)
        {
            AtoViewModel atoViewModel = new AtoViewModel();
            BusinessErrorViewModel businessError = new BusinessErrorViewModel();
            string descMsg = string.Empty;

            try
            {
                if (Id.HasValue)
                {
                    using (AppServiceAtos appService = new AppServiceAtos(this.UfwCartNew, this.IdCtaAcessoSist))
                    {
                        string[] statusEditaveis = appService.StatusEditaveis;
                        string[] statusCamposReadOnly = appService.StatusCamposReadOnly;

                        ViewBag.StatusEditaveis = statusEditaveis;
                        ViewBag.StatusCamposReadOnly = statusCamposReadOnly;

                        DtoAto ato = appService.GetById(Id);
                         
                        if (ato == null)
                        {
                            return RedirectToAction("Error404", "Adm");
                        }

                        //se não for permitido editar
                        if (!statusEditaveis.Contains(ato.StatusAto))
                        {
                            businessError.ListErros.Add("Edição não é permitida para o status atual do ato!");
                        }

                        if (businessError.ListErros.Count >= 1)
                        {
                            descMsg = "Ação não está em conformidade com regras de negócio do sistema. Verifique!";
                            TempData["businessError"] = businessError;
                            return RedirectToAction("BusinessError", "Adm", new { descricao = descMsg });
                        }

                        List<Livro> listaLivro = this.UfwCartNew.Repositories.GenericRepository<Livro>().Get().ToList();
                        ViewBag.listaLivro = new SelectList(listaLivro, "Id", "Descricao");

                        //povoar tree view
                        List<TipoAtoList> listaTipoAto = this.UfwCartNew.Repositories.RepositoryTipoAto.GetListTipoAtos(null).ToList();
                        ViewBag.listaTipoAto = listaTipoAto;

                        atoViewModel = Mapper.Map<DtoAto, AtoViewModel>(ato);
                    }
                } else {
                    //return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                    return RedirectToAction("Error404", "Adm");
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Falha em: {0}.{1} [{2}{3}]", GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message, (ex.InnerException != null) ? "=>" + ex.InnerException.Message : "");
                TempData["excecaoGerada"] = ex;
                return RedirectToAction("InternalServerError", "Adm", new { descricao = msg });
            }
            
            return View(atoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarAto(AtoViewModel modelo)
        {
            string filePath = Server.MapPath($"~/App_Data/Arquivos/AtosPendentes/{modelo.NumMatricula}_pendente.docx");
            bool respEscreverWord = false;
            
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Não é possível bloquear um ato já bloqueado");

            /*
            try
            {
                if (modelo.Id == null)
                {
                    ViewBag.erro = "O Ato é obrigatório";
                    return View(nameof(EditarAto), modelo);
                }

                //todo: ronaldo arrumar ato editar
                //Ajusta a string de ato
                //modelo.Id = RemoveUltimaMarcacao(modelo.Id);

                if (ModelState.IsValid)
                {
                    //Representa o documento e o numero de pagina
                    //DtoCadastroDeAto modeloDto = Mapper.Map<AtoViewModel, DtoCadastroDeAto>(modelo);
                    long? numSequenciaAto = null;

                    if (modelo.NumSequenciaAto == 0 && modelo.IdTipoAto != (int)TipoAtoEnum.AtoInicial)
                    {
                        numSequenciaAto = this.UfwCartNew.Repositories.RepositoryAto.GetNumSequenciaAto(modelo.NumMatricula);
                        numSequenciaAto = numSequenciaAto != null ? numSequenciaAto : 1;
                    } else {
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
                        using (var appService = new AppServiceAtos(this.UfwCartNew, this.IdCtaAcessoSist))
                        {
                            //var dtoEditar = Mapper.Map<AtoViewModel, DtoCadastroDeAto>(modelo);

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
                    return RedirectToActionPermanent(nameof(BloquearAto), new { modelo.Id });
                }

                ViewBag.erro = "Erro ao cadastrar o ato!";

                return View(nameof(EditarAto), modelo);
            }
            catch (Exception ex)
            {
                string msg = string.Format("Falha em: {0}.{1} [{2}{3}]", GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message, (ex.InnerException != null) ? "=>" + ex.InnerException.Message : "");
                TempData["excecaoGerada"] = ex;
                return RedirectToAction("InternalServerError", "Adm", new { descricao = msg });
            }
            */

        }
        #endregion

        public ActionResult BloquearAto(long? Id)
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
                    //else if (Ato.Bloqueado == true)
                    //{
                    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Não é possível bloquear um ato já bloqueado");
                    //}
                    AtoListViewModel atoViewModel = new AtoListViewModel
                    {
                        Id = Ato.Id,
                        Ativo = Ato.Ativo,
                        //NumSequencia = Ato.NumSequencia,
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
                string msg = string.Format("Falha em: {0}.{1} [{2}{3}]", GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message, (ex.InnerException != null) ? "=>" + ex.InnerException.Message : "");
                TempData["excecaoGerada"] = ex;
                return RedirectToAction("InternalServerError", "Adm", new { descricao = msg });
            }
        }

        [HttpPost]
        public void BloquearAto(string NumMatricula, long IdAto)
        {
            try
            {
                using (var appService = new AppServiceAtos(this.UfwCartNew, this.IdCtaAcessoSist))
                {
                    var resultado = false; // appService.FinalizarAto(IdAto);

                    if (resultado)
                    {
                        this.UfwCartNew.SaveChanges();
                        //todo: ronaldo fazer 
                        //WordHelper.EscreverAtoPrincipal(Server.MapPath($"~/App_Data/Arquivos/AtosPendentes/{NumMatricula}_pendente.docx"), Server.MapPath($"~/App_Data/Arquivos/Atos/{NumMatricula}.docx"));
                        Response.StatusCode = 200;
                        Response.Status = "Ato Bloqueado com sucesso!";
                    } else {
                        Response.StatusCode = 500;
                        Response.Status = "Erro ao atualizar o ato!";
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Falha em: {0}.{1} [{2}{3}]", GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message, (ex.InnerException != null) ? "=>" + ex.InnerException.Message : "");
                TempData["excecaoGerada"] = ex;
                RedirectToAction("InternalServerError", "Adm", new { descricao = msg });
            }
        }

        /// <summary>
        /// Lista de Modelos (JSON) por IdTipo
        /// </summary>
        /// <returns>Lista de arquivos</returns>
        [HttpPost]
        public JsonResult GetListModelosDocx(long? IdTipoAto)
        {
            bool resp = false;
            string mesage = string.Empty;
            List<DtoModeloDocxList> lista = new List<DtoModeloDocxList>();

            try
            {
                using (var appService = new AppServiceModelosDoc(this.UfwCartNew, this.IdCtaAcessoSist))
                {
                    lista = appService.GetListModelosDocx(IdTipoAto).ToList();
                    resp = true;
                    mesage = "Dados retornados con sucesso";
                }
            }
            catch (Exception ex)
            {
                mesage = "Falha ao obter dados! " + "[" + ex.Message + "]";
            }

            var resultado = new
            {
                resposta = resp,
                msg = mesage,
                ListaModelosDocx = lista
            };

            return Json(resultado);
        }

        public JsonResult GetDadosPorPrenotacao(long IdPrenotacao) 
        {
            bool resp = false;
            string message = string.Empty;
            DateTime? dataReg = null;

            List<DtoDadosImovel> listaDtoDadosImovel = new List<DtoDadosImovel>();

            try
            {
                using (AppServiceAtos appServAtos = new AppServiceAtos(this.UfwCartNew, this.IdCtaAcessoSist))
                {
                    dataReg = appServAtos.DataRegPrenotacao(IdPrenotacao);
                    listaDtoDadosImovel = appServAtos.GetListImoveisPrenotacao(IdPrenotacao).ToList();

                    if (listaDtoDadosImovel != null)
                    {
                        if (listaDtoDadosImovel.Count() > 0)
                        {
                            message = ":) Dados retornados con sucesso.";
                            resp = true;
                        } else
                        {
                            message = "Número de Prenotação e/ou matrículas não encontradas na base de dados!";
                        }
                    } else
                    {
                        message = "Número de Prenotação Inválido!";
                    }
                }
            }
            catch (Exception ex)
            {
                message = "Falha ao obter dados! " + "[" + ex.Message + "]";
            }

            var resultado = new
            {
                resposta = resp,
                msg = message,
                DataRegPrenotacao =  dataReg.HasValue? dataReg.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) :"",
                listaDtoDadosImovel
            };

            return Json(resultado);
        }

        /// <summary>
        /// Busca dados do imóvel por prenotação
        /// </summary>
        /// <param name="matriculaPrenotacao"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetListImoveisPrenotacao(long IdPrenotacao)
        {
            bool resp = false;
            string message = string.Empty;

            List<DtoDadosImovel> listaDtoDadosImovel = new List<DtoDadosImovel>();

            try
            {
                using (AppServiceAtos appServAtos = new AppServiceAtos(this.UfwCartNew, this.IdCtaAcessoSist))
                {
                    listaDtoDadosImovel = appServAtos.GetListImoveisPrenotacao(IdPrenotacao).ToList();

                    if (listaDtoDadosImovel != null)
                    {
                        if (listaDtoDadosImovel.Count() > 0)
                        {
                            message = "Dados retornados con sucesso :)";
                            resp = true;
                        } else {
                            message = "Número de Prenotação e/ou matrículas não encontradas na base de dados!";
                        }
                    } else {
                        message = "Número de Prenotação Inválido!";
                    }
                }
            }
            catch (Exception ex)
            {
                message = "Falha ao obter dados! " + "[" + ex.Message + "]";
            }

            var resultado = new
            {
                resposta = resp,
                msg = message,
                listaDtoDadosImovel
            };

            return Json(resultado);
        }

        /// <summary>
        /// Essa função retorna uma lista de pessoa por um id de prenotação
        /// </summary>
        /// <param name="IdPrenotacao">Numero da prenotação SEQPRE</param>
        /// <returns>JSON</returns>
        [HttpPost]
        public JsonResult GetListPessoasPrenotacao(long IdPrenotacao)
        {
            bool resp = false;
            string message = string.Empty;
            IEnumerable<DtoPessoaPesxPre> listaPes = new List<DtoPessoaPesxPre>();

            try
            {
                using (AppServiceAtos appServiceAtos = new AppServiceAtos(this.UfwCartNew, this.IdCtaAcessoSist))
                {
                    listaPes = appServiceAtos.GetListPessoasPrenotacao(IdPrenotacao);
                    resp = true;
                    message = "Lista de pessoas da prenotação obtida com sucesso!";
                }
            }
            catch (Exception ex)
            {
                resp = false;
                message = string.Format("{0}.{1} [{2} => {3}]", GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");

                //    Console.WriteLine(ex);
                //    Response.StatusCode = 500;
                //    Response.Status = "Erro ao buscar os dados das pessoas";
            }

            //JsonConvert.SerializeObject()

            var resultado = new
            {
                resposta = resp,
                msg = message,
                listaPessoas = listaPes
            };

            return Json(resultado);
        }

        public bool ExisteAto(string NumMatricula)
        {
            try
            {
                //string filePath = Server.MapPath($"~/App_Data/Arquivos/Atos/{NumMatricula}.docx");
                //using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                //{

                //}
                Response.StatusCode = 200;
                return true;
            }
            catch (FileNotFoundException ex)
            {
                Response.StatusCode = 200;
                Response.Write(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.Write(ex.Message);
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
                Response.StatusCode = 500;
                Response.Write(ex.Message);
                return null;
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
                Response.StatusCode = 500;
                Response.Write(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Retorn o texto de um modelo
        /// </summary>
        /// <param name="IdModeloDoc"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetTextoWordDocModelo(long IdModeloDoc)
        {
            bool resp = false;
            StringBuilder texto = new StringBuilder();
            string message = string.Empty;

            try
            {
                string serverPath = Server.MapPath("~");

                using (AppServiceAtos appServ = new AppServiceAtos(this.UfwCartNew, this.IdCtaAcessoSist))
                {
                    texto = appServ.GetTextoWordDocModelo(IdModeloDoc, serverPath);
                }

                resp = true;
            }
            catch (Exception ex)
            {
                message = string.Format("Falha em: {0}.{1} [{2}{3}]", GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message, (ex.InnerException != null) ? "=>" + ex.InnerException.Message : "");
            }

            var resultado = new
            {
                resposta = resp,
                msg = message,
                TextoHtml = texto.ToString()
            };

            return Json(resultado);
        }

        /// <summary>
        /// Povoar texto do ckEditorAto com texto vindo do modelo 
        /// e os dados lidos do imovel e pessoas 
        /// </summary>
        /// <returns>string HTML</returns>
        [HttpPost]
        public JsonResult GetTextoAto(InfAtoViewModel dadosAtoViewModel)
        {
            bool resp = false;
            string message = string.Empty;
            string texto = string.Empty;

            try
            {
                if (dadosAtoViewModel.IdModeloDoc == 0)
                {
                    throw new NullReferenceException("Modelo de documento não definido!");
                }

                string serverPath = Server.MapPath("~");

                using (AppServiceAtos appServiceAtos = new AppServiceAtos(this.UfwCartNew, this.IdCtaAcessoSist))
                {
                    DtoInfAto dtoInfAto = new DtoInfAto
                    {
                        IdAto = dadosAtoViewModel.IdAto,
                        IdCtaAcessoSist = this.IdCtaAcessoSist,
                        IdTipoAto = dadosAtoViewModel.IdTipoAto,
                        IdLivro = dadosAtoViewModel.IdLivro,
                        IdPrenotacao = dadosAtoViewModel.IdPrenotacao,
                        IdModeloDoc = dadosAtoViewModel.IdModeloDoc,
                        NumMatricula = dadosAtoViewModel.NumMatricula,
                        ServerPath = serverPath,
                        ListIdsPessoas = dadosAtoViewModel.ListIdsPessoas
                    };

                    texto = appServiceAtos.GetTextoAto(dtoInfAto).ToString();
                }

                resp = true;
            }
            catch (Exception ex)
            {
                resp = false;
                message = string.Format("Falha em: {0}.{1} [{2}{3}]", GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message, (ex.InnerException != null) ? "=>" + ex.InnerException.Message : "");
            }

            var resultado = new
            {
                resposta = resp,
                msg = message,
                TextoHtml = texto
            };

            return Json(resultado);
        }

        public JsonResult ProcReservarMatImovel(TipoReservaMatImovel TipoReserva, long IdPrenotacao, string NumMatricula) 
        {
            bool resp = false;
            string message = string.Empty;
            DtoReservaImovel reservaImovel = new DtoReservaImovel();
            List<ApplicationUser> listaUsrSist = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.OrderBy(u => u.UserName).ToList();

            try
            {
                using (AppServiceAtos appServiceAtos = new AppServiceAtos(this.UfwCartNew, this.IdCtaAcessoSist))
                {
                    appServiceAtos.ListaUsuariosSistema = listaUsrSist;
                    reservaImovel = appServiceAtos.ProcReservarMatImovel(TipoReserva, IdPrenotacao, NumMatricula, this.UsuarioAtual.Id);
                }

                resp = reservaImovel.Resposta;
                message = reservaImovel.Msg;
            }
            catch (Exception ex)
            {
                resp = false;
                message = string.Format("Falha em: {0}.{1} [{2}{3}]", GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message, (ex.InnerException != null) ? "=>" + ex.InnerException.Message : "");
            }

            var resultado = new
            {
                resposta = resp,
                operacao = reservaImovel.Operacao,
                tipoMsg = reservaImovel.TipoMsg,
                msg = message,
                Reserva = reservaImovel
            };

            return Json(resultado);
        }

    }
}
