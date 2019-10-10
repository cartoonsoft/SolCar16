using System;
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
using Domain.CartNew.Interfaces.UnitOfWork;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using Dto.CartNew.Entities.Cart_11RI;
using LibFunctions.Functions.IOAdmCartorio;
using Domain.CartNew.Entities.Diversos;
using GemBox.Document;
using Cartorio11RI.Cartorio;

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

        // GET: Ato
        public ActionResult IndexAto(DateTime? DataIni = null, DateTime? DataFim = null)
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
                    IEnumerable<DtoAtoList> listaDto = appService.GetListaAtos((DateTime)DataIni, (DateTime)DataFim).Where(a => a.Ativo == true);
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

        #region |NovoAto|
        public ActionResult NovoAto()
        {
            var dados = new AtoViewModel();
            List<Livro> listaLivro = this.UfwCartNew.Repositories.GenericRepository<Livro>().Get().ToList();

            //povoar tree view
            List<TipoAtoList> listaTipoAto = this.UfwCartNew.Repositories.RepositoryTipoAto.ListaTipoAtos(null).ToList();
            ViewBag.listaTipoAto = listaTipoAto; // new SelectList(listaTipoAto, "Id", "Descricao");


            ViewBag.listaLivro = new SelectList(listaLivro, "Id", "Descricao");
            ViewBag.listaModelosDocx = new SelectList(
                new[] { new { IdModeloDoc = "0", NomeModelo = "Selecione um modelo" } },
                "IdModeloDoc",
                "NomeModelo"
            );

            return View(dados);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult NovoAto(AtoViewModel modelo)
        {
            string filePath = Server.MapPath($"~/App_Data/Arquivos/AtosPendentes/{modelo.PREIMO.MATRI}_pendente.docx");
            bool respEscreverWord = false;
            Ato ato;
            try
            {
                //povoar tree view
                List<TipoAtoList> listaTipoAto = this.UfwCartNew.Repositories.RepositoryTipoAto.ListaTipoAtos(null).ToList();
                ViewBag.listaTipoAto = listaTipoAto; // new SelectList(listaTipoAto, "Id", "Descricao");

                //throw new Exception("Teste Ronaldo");

                if (modelo.Id == null)
                {
                    ViewBag.erro = "O Ato é obrigatório";
                    return View(modelo);
                }

                //Ajusta a string de ato
                //modelo.Id??0 = RemoveUltimaMarcacao("" /*modelo.Id.ToString()*/);  //todo: ronalod arrumar 

                if (ModelState.IsValid)
                {
                    //Representa o documento e o numero de pagina
                    //DtoCadastroDeAto modeloDto = Mapper.Map<AtoViewModel, DtoCadastroDeAto>(modelo);
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

                return View(modelo);
            }
            catch (Exception ex)
            {
                TypeInfo t = this.GetType().GetTypeInfo();
                IOFunctions.GerarLogErro(t, ex);
                return RedirectToAction("InternalServerError", "Adm", new { excecao = ex });
            }
        }
        #endregion
        
        #region |EditarAto|
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
                    //else if (Ato.Bloqueado == true)
                    //{
                    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Não é possível editar um ato já bloqueado.");
                    //}
                    AtoViewModel atoViewModel = new AtoViewModel()
                    {
                        Id = Id,
                        PREIMO = new PREIMOViewModel()
                        {
                            SEQIMO = Convert.ToInt64(Ato.NumMatricula),
                            SEQPRE = Ato.IdPrenotacao
                        },
                        NumSequenciaAto = Ato.NumSequenciaAto
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
                    //DtoCadastroDeAto modeloDto = Mapper.Map<AtoViewModel, DtoCadastroDeAto>(modelo);
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
                    //else if (Ato.Bloqueado == true)
                    //{
                    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Não é possível bloquear um ato já bloqueado");
                    //}
                    AtoListViewModel atoViewModel = new AtoListViewModel
                    {
                        Id = Ato.Id,
                        Ativo = Ato.Ativo,
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


        public PartialViewResult PartialDadosAdicionais()
        {
            return PartialView();
        }

        public PartialViewResult PartialDadosPessoas(string listaPessoas)
        {
            var dados = JsonConvert.DeserializeObject<List<DadosPessoaViewModel>>(listaPessoas);
            return PartialView(dados);
        }

        /// <summary>
        /// Lista de Modelos (JSON) por IdTipo
        /// </summary>
        /// <returns>Lista de arquivos</returns>
        [HttpPost]
        public JsonResult GetListaModelosDocx(long? IdTipoAto)
        {
            bool resp = false;
            string mesage = string.Empty;
            List<DtoModeloDocxList> lista = new List<DtoModeloDocxList>();

            try
            {
                using (var appService = new AppServiceModelosDoc(this.UfwCartNew))
                {
                    lista = appService.GetListaModelosDocx(IdTipoAto).ToList();
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

        /// <summary>
        /// Busca dados do imóvel por prenotação
        /// </summary>
        /// <param name="matriculaPrenotacao"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetDadosImovelPrenotacao(long IdPrenotacao)
        {
            bool resp = false;
            string message = string.Empty;

            DtoDadosImovel dtoDadosImovel  = new DtoDadosImovel();

            try
            {
                using (AppServiceAtos appServAtos = new AppServiceAtos(this.UfwCartNew))
                {
                    dtoDadosImovel = appServAtos.GetDadosImovelPrenotacao(IdPrenotacao);
                    if (dtoDadosImovel != null)
                    {
                        message = "Dados retornados con sucesso";
                    } else
                    {
                        message = "Número de Prenotação não encontrada na base de dados";
                    }
                }
                resp = true;
            }
            catch (Exception ex)
            {
                message = "Falha ao obter dados! " + "[" + ex.Message + "]";
            }

            var resultado = new
            {
                resposta = resp,
                msg = message,
                dtoDadosImovel = dtoDadosImovel
            };

            return Json(resultado);
        }

        /// <summary>
        /// Essa função retorna uma lista de pessoa por um id de prenotação
        /// </summary>
        /// <param name="numeroPrenotacao">Numero da prenotação</param>
        /// <returns>JSON</returns>
        [HttpPost]
        public JsonResult GetPessoasPrenotacao(long numeroPrenotacao)
        {
            bool resp = false;
            string message = string.Empty;
            IEnumerable<DtoPessoaPesxPre> listaPes = new List<DtoPessoaPesxPre>();

            try
            {
                using (AppServiceAtos appServiceAtos = new AppServiceAtos(this.UfwCartNew))
                {
                    listaPes = appServiceAtos.GetPessoasPrenotacao(numeroPrenotacao);
                    resp = true;
                    message = "Lista de pessoas da prenotação obtida com sucesso!";
                }
            }
            catch (Exception ex)
            {
                resp = false;
                message = "Falha, GetPessoasPrenotacao [" + ex.Message + "]";
                //    Console.WriteLine(ex);
                //    Response.StatusCode = 500;
                //    Response.Status = "Erro ao buscar os dados das pessoas";
                //
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

        /// <summary>
        /// GetTextoWordDocModelo
        /// </summary>
        /// <param name="IdModeloDoc"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetTextoWordDocModelo(long IdModeloDoc)
        {
            bool resp = false;
            string texto = string.Empty;
            string message = string.Empty;
            
            try
            {
                FilesConfig files = new FilesConfig(this.IdCtaAcessoSist);
                string fileName = files.GetModeloDocFileName(IdModeloDoc);
                string fullName = Server.MapPath("~" + fileName);

                using (AppServiceAtos appServiceAtos = new AppServiceAtos(this.UfwCartNew))
                {
                    texto = appServiceAtos.GetTextoWordDocModelo(fullName).ToString();
                }


                resp = true;
            }
            catch (Exception ex)
            {
                message = "Falha GetTextoWordDocModelo[" + ex.Message + "]";
            }

            var resultado = new
            {
                resposta = resp,
                msg = message,
                TextoHtml = texto
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

                FilesConfig files = new FilesConfig(this.IdCtaAcessoSist);
                string fileName = files.GetModeloDocFileName(dadosAtoViewModel.IdModeloDoc);
                string fullName = Server.MapPath("~" + fileName);

                using (AppServiceAtos appServiceAtos = new AppServiceAtos(this.UfwCartNew))
                {
                    DtoInfAto dtoInfAto = new DtoInfAto {
                        IdAto = dadosAtoViewModel.IdAto,
                        IdCtaAcessoSist = this.IdCtaAcessoSist,
                        IdTipoAto = dadosAtoViewModel.IdTipoAto,
                        IdPrenotacao = dadosAtoViewModel.IdPrenotacao,
                        IdModeloDoc = dadosAtoViewModel.IdModeloDoc,
                        NumMatricula = dadosAtoViewModel.NumMatricula,
                        ListIdsPessoas= dadosAtoViewModel.ListIdsPessoas
                    };

                    texto = appServiceAtos.GetTextoAto(dtoInfAto).ToString();
                }

                texto = "Teste 123...";
                resp = true;
            }
            catch (Exception ex)
            {
                resp = false;
                message = "Falha, GetTextoAto [" + ex.Message + "]";
            }

            var resultado = new
            {
                resposta = resp,
                msg = message,
                TextoHtml = texto
            };

            return Json(resultado);
        }
    }
}
