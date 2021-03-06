﻿using System;
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
            IEnumerable<ModeloDocListViewModel> listaArquivoModeloDocxListViewModel = new List<ModeloDocListViewModel>();

            try
            {
                using (AppServiceModelosDoc appService = new AppServiceModelosDoc(this.UfwCartNew, this.IdCtaAcessoSist))
                {
                    IEnumerable<DtoModeloDocxList> listaDtoModelosDocx = appService.GetListModelosDocx(null);
                    listaArquivoModeloDocxListViewModel = Mapper.Map<IEnumerable<DtoModeloDocxList>, IEnumerable<ModeloDocListViewModel>>(listaDtoModelosDocx);
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
            ModeloDocViewModel model = new ModeloDocViewModel();

            try
            {
                List<TipoAtoList> listaTipoAto = this.UfwCartNew.Repositories.RepositoryTipoAto.GetListTipoAtos(null).ToList();
                ViewBag.listaTipoAto = listaTipoAto; 
                ViewBag.listaCampoTipoAto = new SelectList(new List<CampoTipoAto>(), "Id", "NomeCampo");
                model.IdCtaAcessoSist = this.IdCtaAcessoSist;
                model.Ativo = true;
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
        public ActionResult NovoModelo(ModeloDocViewModel modeloDocxViewModel)
        {
            string msg = string.Empty;
            long? NovoId;

            try
            {
                List<TipoAtoList> listaTipoAto = this.UfwCartNew.Repositories.RepositoryTipoAto.GetListTipoAtos(null).ToList();
                ViewBag.listaTipoAto = listaTipoAto;
                ViewBag.listaCampoTipoAto = new SelectList(new List<CampoTipoAto>(), "Id", "NomeCampo");

                if (ModelState.IsValid)
                {
                    string filePath = string.Empty;

                    using (AppServiceModelosDoc appService = new AppServiceModelosDoc(this.UfwCartNew, this.IdCtaAcessoSist))
                    {
                        NovoId = appService.NovoModelo(
                            new DtoModeloDoc()
                            {
                                IdCtaAcessoSist = this.IdCtaAcessoSist,
                                IdTipoAto = modeloDocxViewModel.IdTipoAto,
                                IdUsuarioCadastro = this.UsuarioAtual.Id,
                                DataCadastro = DateTime.Now,
                                Descricao = modeloDocxViewModel.Descricao,
                                Texto = modeloDocxViewModel.Texto,
                                Orientacao = modeloDocxViewModel.Orientacao,
                                UsuarioSistOperacional = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                                IpLocal = modeloDocxViewModel.IpLocal,
                                Ativo = true
                            }
                        ); 
                    }

                    return RedirectToActionPermanent(nameof(IndexModelo));
                }
            }
            catch (Exception ex)
            {
                msg = string.Format("Falha em: {0}.{1} [{2}{3}]", GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message, (ex.InnerException != null) ? "=>" + ex.InnerException.Message : "");
                ModelState.AddModelError(Guid.NewGuid().ToString(), msg);
                TempData["error"] = ex;

                return RedirectToAction("InternalServerError", "Adm", new { descricao = msg });
            }

            return View(modeloDocxViewModel);
        }

        // GET: Modelo/Editar/{ID}
        public ActionResult EditarModelo(long? Id)
        {
            ModeloDocViewModel modeloDocxViewModel = new ModeloDocViewModel();

            if (Id.HasValue && Id > 0)
            {
                try
                {
                    using (var appService = new AppServiceModelosDoc(this.UfwCartNew, this.IdCtaAcessoSist))
                    {
                        DtoModeloDoc dtoModelo =  appService.GetById(Id);

                        if (dtoModelo == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                        }

                        modeloDocxViewModel = Mapper.Map<DtoModeloDoc, ModeloDocViewModel>(dtoModelo);
                    }

                    List<TipoAtoList> listaTipoAto = this.UfwCartNew.Repositories.RepositoryTipoAto.GetListTipoAtos(null).ToList();
                    ViewBag.listaTipoAto = listaTipoAto; // new SelectList(listaTipoAto, "Id", "Descricao");
                    List<CampoTipoAto> listaCampoTipoAto = this.UfwCartNew.Repositories.RepositoryTipoAto.GetListCamposTipoAto(modeloDocxViewModel.IdTipoAto, this.IdCtaAcessoSist).ToList();
                    ViewBag.listaCampoTipoAto = new SelectList(listaCampoTipoAto, "Id", "NomeCampo");

                    return View(modeloDocxViewModel);
                }
                catch (Exception ex)
                {
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
        public ActionResult EditarModelo(ModeloDocViewModel modeloDocxViewModel)
        {
            string msg = string.Empty;

            try
            {
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
                            IdUsuarioAlteracao = this.UsuarioAtual.Id,
                            DataAlteracao = DateTime.Now,
                            Descricao = modeloDocxViewModel.Descricao,
                            Texto = modeloDocxViewModel.Texto,
                            Orientacao = modeloDocxViewModel.Orientacao,
                            IpLocal = modeloDocxViewModel.IpLocal,
                            UsuarioSistOperacional = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                            Ativo = modeloDocxViewModel.Ativo
                        });
                    }

                    return RedirectToActionPermanent(nameof(IndexModelo));
                }

                return View(modeloDocxViewModel);
            }
            catch (Exception ex)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                msg = string.Format("Falha em: {0}.{1} [{2}{3}]", GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message, (ex.InnerException != null) ? "=>" + ex.InnerException.Message : "");
                TempData["excecaoGerada"] = ex;
                return RedirectToAction("InternalServerError", "Adm", new { descricao = msg });
            }
        }

        [ChildActionOnly]
        public PartialViewResult PartialFormModeloDoc(ModeloDocViewModel modeloDocxViewModel)
        {
            return PartialView("_frmModeloDocx", modeloDocxViewModel);
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
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch (Exception)
            {
                //return RedirectToAction("InternalServerError", "Adm", new { excecao = ex });
                return null;
            }
        }

        [HttpPost]
        public JsonResult GetListCamposTipoAto(long? IdTipoAto, string entidade = null)
        {
            List<CampoTipoAto> lista = new List<CampoTipoAto>();
            bool resp = false;
            string message = string.Empty;
            
            try
            {
                lista = this.UfwCartNew.Repositories.RepositoryTipoAto.GetListCamposTipoAto(IdTipoAto, this.IdCtaAcessoSist, entidade).ToList();
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
                ListaCamposTipoAto = lista
            };

            return Json(resultado);
        }

    }

}
