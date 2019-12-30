using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Domain.CartNew.Enumerations;
using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.UnitOfWork;
using Cartorio11RI.Controllers.Base;
using Cartorio11RI.ViewModels;
using AppServCart11RI.AppServices;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using Dto.CartNew.Entities.Cart_11RI;
using System.Net.Sockets;
using Domain.CartNew.Entities.Diversos;
using System.Reflection;
using LibFunctions.Functions.IOAdmCartorio;

namespace Cartorio11RI.Controllers
{
    /// <summary>
    /// Modelos de documento do word docx
    /// </summary>
    [Authorize]
    public class ModelosController : CartorioBaseController
    {
        public ModelosController(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            //
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //coloque aqui os app servics para dar dispose
            }
            base.Dispose(disposing);
        }

        // GET: Arquivos
        public ActionResult IndexModelo()
        {
            IEnumerable<ModeloDocxListViewModel> listaArquivoModeloDocxListViewModel = new List<ModeloDocxListViewModel>();

            try
            {
                using (AppServiceModelosDoc appService = new AppServiceModelosDoc(this.UfwCartNew, this.IdCtaAcessoSist))
                {
                    IEnumerable<DtoModeloDocxList> listaDtoModelosDocx = appService.GetListaModelosDocx(null).Where(a => a.Ativo == true);
                    listaArquivoModeloDocxListViewModel = Mapper.Map<IEnumerable<DtoModeloDocxList>, IEnumerable<ModeloDocxListViewModel>>(listaDtoModelosDocx);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Falha em: {0}.{1} [{2}{3}]", GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message, (ex.InnerException != null) ? "=>" + ex.InnerException.Message : "");
                TempData["excecaoGerada"] = ex;
                return RedirectToAction("InternalServerError", "Adm", new { descricao = msg });
            }

            return View(listaArquivoModeloDocxListViewModel);
        }

        // GET: Modelo/Novo
        public ActionResult NovoModelo()
        {
            ModeloDocxViewModel model = new ModeloDocxViewModel();

            try
            {
                List<TipoAtoList> listaTipoAto = this.UfwCartNew.Repositories.RepositoryTipoAto.GetListTipoAtos(null).ToList();
                ViewBag.listaTipoAto = listaTipoAto; // new SelectList(listaTipoAto, "Id", "Descricao");

                model.IdCtaAcessoSist = this.IdCtaAcessoSist;
                model.IdUsuarioCadastro = this.UsuarioAtual.Id;
                model.DescricaoTipoAto = "";
            }
            catch (Exception ex)
            {
                string msg = string.Format("Falha em: {0}.{1} [{2}{3}]", GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message, (ex.InnerException != null) ? "=>" + ex.InnerException.Message : "");
                TempData["excecaoGerada"] = ex;
                return RedirectToAction("InternalServerError", "Adm", new { descricao = msg });
            }

            return View(model);
        }

        // POST: Modelo/Novo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovoModelo([Bind(Include = "Id,IdCtaAcessoSist,IdTipoAto,IdUsuarioCadastro,IdUsuarioAlteracao,DataCadastro,DataAlteracao,DescricaoModelo," +
            "DescricaoTipoAto,Files,CaminhoEArquivo,IpLocal,Ativo")] ModeloDocxViewModel modeloDocxViewModel)
        {
            bool ControllerModelValid = ModelState.IsValid;
            bool success = false;
            string msg = "";
            long? NovoId;

            try
            {
                //throw new Exception("Deu errro :(");

                List<TipoAtoList> listaTipoAto = this.UfwCartNew.Repositories.RepositoryTipoAto.GetListTipoAtos(null).ToList();
                ViewBag.listaTipoAto = listaTipoAto;

                if (ControllerModelValid)
                {
                    string filePath = string.Empty;
                    modeloDocxViewModel.CaminhoEArquivo = Server.MapPath("~/App_Data/Arquivos/Modelos/");

                    using (AppServiceModelosDoc appService = new AppServiceModelosDoc(this.UfwCartNew, this.IdCtaAcessoSist))
                    {
                        NovoId = appService.NovoModelo(
                            new DtoModeloDoc()
                            {
                                IdCtaAcessoSist = this.IdCtaAcessoSist,
                                IdTipoAto = modeloDocxViewModel.IdTipoAto,
                                CaminhoEArquivo = modeloDocxViewModel.CaminhoEArquivo, // Path.Combine(Server.MapPath("~/App_Data/Arquivos/Modelos/"), NovoId.ToString() + ".docx"),
                                ArquivoDocxModelo = modeloDocxViewModel.ArquivoDocxModelo,
                                DescricaoModelo = modeloDocxViewModel.DescricaoModelo,
                                UsuarioSistOperacional = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                                IpLocal = modeloDocxViewModel.IpLocal,
                                Ativo = true
                            },
                            this.UsuarioAtual.Id
                        );
                    }

                    if (modeloDocxViewModel.ArquivoDocxModelo != null && modeloDocxViewModel.ArquivoDocxModelo.ContentLength > 0)
                    {
                        //Pega os dados do arquivo
                        HttpPostedFileBase arquivo = modeloDocxViewModel.ArquivoDocxModelo;

                        //arquivo.FileName = "Mod_"+arquivoModel.DescricaoTipoAto+DateTime.Now.ToString("yyyyMMddTHHmmss")
                        var nomeArquivo = Path.GetFileNameWithoutExtension(arquivo.FileName);

                        // Salva o arquivo fisicamente
                        filePath = Path.Combine(modeloDocxViewModel.CaminhoEArquivo, "modelo_" + NovoId.ToString() + ".docx");
                        arquivo.SaveAs(filePath);
                    }

                    //ModelState.AddModelError(Guid.NewGuid().ToString(), "Erro generico");
                    msg = "Modelo adicionado com sucesso!";
                    success = true;
                }
            }
            catch (Exception ex)
            {
                //System.Diagnostics.Debug.WriteLine("ArquivosController Exception: " + ex.Message);
                //return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                //return RedirectToAction("InternalServerError", "Adm", new { excecao = ex });

                msg = string.Format("Falha em: {0}.{1} [{2}{3}]", GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message, (ex.InnerException != null) ? "=>" + ex.InnerException.Message : "");
                ModelState.AddModelError(Guid.NewGuid().ToString(), msg);
                TempData["error"] = ex;

                return RedirectToAction("InternalServerError", "Adm", new { descricao = msg });
            }

            //var resultado = new
            //{
            //    success = success,
            //    mensagem = msg
            //};

            //return Json(resultado, JsonRequestBehavior.AllowGet);
            ViewBag.success = success ? "true" : "false";
            ViewBag.ControllerModelValid = ControllerModelValid ? "true" : "false";
            ViewBag.msg = msg;

            return View(modeloDocxViewModel);
        }

        // GET: Modelo/Editar/{ID}
        public ActionResult EditarModelo(long? Id)
        {
            if (Id.HasValue && Id > 0)
            {
                try
                {
                    ///povoar tree view
                    List<TipoAtoList> listaTipoAto = this.UfwCartNew.Repositories.RepositoryTipoAto.GetListTipoAtos(null).ToList();
                    ViewBag.listaTipoAto = listaTipoAto; // new SelectList(listaTipoAto, "Id", "Descricao");

                    ModeloDoc modeloDocx = this.UfwCartNew.Repositories.RepositoryModeloDocx.GetById(Id);
                    ModeloDocxViewModel modeloDocxViewModel = Mapper.Map<ModeloDoc, ModeloDocxViewModel>(modeloDocx);
                    modeloDocxViewModel.DescricaoTipoAto = this.UfwCartNew.Repositories.RepositoryTipoAto.Get().Where(t => t.Id == modeloDocx.IdTipoAto).FirstOrDefault().Descricao;

                    if (modeloDocxViewModel == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                    }

                    return View(modeloDocxViewModel);
                }
                catch (Exception ex)
                {
                    //System.Diagnostics.Debug.WriteLine(ex.Message);
                    //return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                    //ModelState.AddModelError(Guid.NewGuid().ToString(), msg);
                    string msg = string.Format("Falha em: {0}.{1} [{2}{3}]", GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message, (ex.InnerException != null) ? "=>" + ex.InnerException.Message : "");
                    TempData["excecaoGerada"] = ex;
                    return RedirectToAction("InternalServerError", "Adm", new { descricao = msg });
                }
            } else {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarModelo(ModeloDocxViewModel modeloDocxViewModel)
        {
            try
            {
                List<TipoAtoList> listaTipoAto = this.UfwCartNew.Repositories.RepositoryTipoAto.GetListTipoAtos(null).ToList();
                ViewBag.listaTipoAto = listaTipoAto; // new SelectList(listaTipoAto, "Id", "Descricao");

                if (ModelState.IsValid)
                {
                    // Fazendo Upload do arquivo
                    using (var appService = new AppServiceModelosDoc(this.UfwCartNew, this.IdCtaAcessoSist))
                    {
                        appService.EditarModelo(new DtoModeloDoc()
                        {
                            Id = modeloDocxViewModel.Id,
                            IdCtaAcessoSist = this.IdCtaAcessoSist,
                            IdTipoAto = modeloDocxViewModel.IdTipoAto,
                            CaminhoEArquivo = modeloDocxViewModel.CaminhoEArquivo, // Path.Combine(Server.MapPath("~/App_Data/Arquivos/Modelos/"), NovoId.ToString() + ".docx"),
                            ArquivoDocxModelo = modeloDocxViewModel.ArquivoDocxModelo,
                            DescricaoModelo = modeloDocxViewModel.DescricaoModelo,
                            IpLocal = modeloDocxViewModel.IpLocal,
                            UsuarioSistOperacional = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                            Ativo = modeloDocxViewModel.Ativo
                        }, UsuarioAtual.Id);
                    }

                    //UploadArquivo(arquivoModeloDocxViewModel);

                    //ViewBag.resultado = "Arquivo salvo com sucesso!";
                    return RedirectToActionPermanent(nameof(IndexModelo));
                }
                return View(modeloDocxViewModel);
            }
            catch (Exception ex)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                string msg = string.Format("Falha em: {0}.{1} [{2}{3}]", GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message, (ex.InnerException != null) ? "=>" + ex.InnerException.Message : "");
                TempData["excecaoGerada"] = ex;
                return RedirectToAction("InternalServerError", "Adm", new { descricao = msg });
            }
        }

        [ChildActionOnly]
        public PartialViewResult PartialFormModeloDoc(ModeloDocxViewModel modeloDocxViewModel)
        {
            return PartialView("_frmModeloDocx", modeloDocxViewModel);
        }

        private void CadastrarLogDownload(string IP, long Id)
        {
            var arquivolog = new LogModeloDoc()
            {
                IdModeloDoc = Id,
                IdUsuario = UsuarioAtual.Id,
                DataHora = DateTime.Now,
                IP = IP,
                TipoLogModeloDoc = TipoLogModeloDoc.Download
            };

            //todo: Ronaldo criar rotina de cadastro 

            return;
        }

        //[ValidateAntiForgeryToken]
        public void DesativarModelo([Bind(Include = "Id, Ip")]DadosPostModeloDocxDownload dadosPost)
        {
            bool respDesativar;

            using (AppServiceModelosDoc appService = new AppServiceModelosDoc(this.UfwCartNew, this.IdCtaAcessoSist))
            {
                respDesativar = appService.Desativar(Convert.ToInt64(dadosPost.Id), this.UsuarioAtual.Id);
            }

            if (!respDesativar)
            {
                throw new Exception("Não foi possível desativar o modelo");
            }
        }

        public FileResult DownloadFile([Bind(Include = "Id, Ip")]DadosPostModeloDocxDownload dadosPost)
        {
            string fileName = "modelo_" + dadosPost.Id.ToString() + ".docx";
            string filePath = Server.MapPath($"~/App_Data/Arquivos/Modelos/modelo_{dadosPost.Id}.docx");

            try
            {
                string ipAddress = dadosPost.Ip;
                if (string.IsNullOrEmpty(ipAddress))
                {

                    var host = Dns.GetHostEntry(Dns.GetHostName());
                    foreach (var ip in host.AddressList)
                    {
                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipAddress = ip.ToString();
                        }
                    }
                }

                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                //Cadastro de LOG
                CadastrarLogDownload(ipAddress, dadosPost.Id);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch (Exception)
            {
                //return RedirectToAction("InternalServerError", "Adm", new { excecao = ex });
                return null;
            }
        }

    }

}
