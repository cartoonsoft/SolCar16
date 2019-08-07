using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.UnitOfWork;
using AdmCartorio.Controllers.Base;
using AdmCartorio.ViewModels;
using AdmCartorio.Models.Identity.Entities;
using AdmCartorio.App_Start.Identity;
using AppServCart11RI.AppServices;

namespace AdmCartorio.Controllers
{
    [Authorize]
    public class AcoesController : CartorioBaseController
    {
        #region | Construtores |
        public AcoesController() : base(null)
        {
            //

        }

        public AcoesController(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            //
        }
        #endregion

        // GET: Acessos
        public ActionResult Index()
        {
            List<ApplicationUser> listaUsrSist = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.Where(u => u.Ativo == true).ToList();
            List<AcaoViewModel> listaAcoes  = new List<AcaoViewModel>();

            using (AppServiceAcoesUsuarios appServAcoesUsuarios = new AppServiceAcoesUsuarios(this.UfwCartNew))
            {
                IEnumerable<Acao> listaTodasAcoes = appServAcoesUsuarios.UfwCartNew.Repositories.GenericRepository<Acao>().GetAll().OrderBy(a => a.DescricaoPequeno);

                foreach (var acao in listaTodasAcoes) 
                {
                    List<UsuarioAcaoViewModel> listaUsrAcao  = new List<UsuarioAcaoViewModel>();
                    var listaUsers = appServAcoesUsuarios.UfwCartNew.Repositories.GenericRepository<UsuarioAcao>().GetWhere(u => u.IdAcao == acao.Id).ToList();

                    foreach (var usuario in listaUsers)
                    {
                        var usrtmp = listaUsrSist.Find(u => u.Id == usuario.IdUsuario);

                        if (usrtmp != null)
                        {
                            listaUsrAcao.Add(new UsuarioAcaoViewModel
                            {
                                IdUsuario = usuario.IdUsuario,
                                UserName = usrtmp.UserName,
                                Email = usrtmp.Email,
                                Nome = usrtmp.Nome
                            });
                        }
                    }

                    listaAcoes.Add(new AcaoViewModel {
                        Id = acao.Id,
                        IdContaAcessoSistema = acao.IdContaAcessoSistema,
                        SeqAcesso            = acao.SeqAcesso,        
                        Programa             = acao.Programa,
                        Obs                  = acao.Obs,
                        DescricaoPequeno     = acao.DescricaoPequeno, 
                        DescricaoMedio       = acao.DescricaoMedio,   
                        DescricaoGrande      = acao.DescricaoGrande,  
                        DescricaoTip         = acao.DescricaoTip,
                        DescricaoBalao       = acao.DescricaoBalao,   
                        Orientacao           = acao.Orientacao,
                        Action               = acao.Action,
                        Controller           = acao.Controller,
                        Parametros           = acao.Parametros,
                        IconeWeb             = acao.IconeWeb,
                        IconeMobile          = acao.IconeMobile,
                        Ativo                = acao.Ativo,
                        EmManutencao         = acao.EmManutencao,
                        ListaUsersAcao       = listaUsrAcao
                    });
                }

                //listAcessos = Mapper.Map<IEnumerable<DtoAcesso>, IEnumerable<ACESSOViewModel>>(null);
            }

            ViewBag.listaUsuarios = new SelectList(listaUsrSist, "Id", "Nome");

            return View(listaAcoes);
        }

        public JsonResult AddUsrAcao(long IdAcao, string IdUsuario)
        {
            bool resposta = false;
            string msg = string.Empty;
            string nome = string.Empty;
            string email = string.Empty;

            try
            {
                using (AppServiceAcoesUsuarios appServAcoesUsuarios = new AppServiceAcoesUsuarios(this.UfwCartNew))
                {
                    Acao acesso = appServAcoesUsuarios.UfwCartNew.Repositories.GenericRepository<Acao>().GetWhere(a => a.Id == IdAcao).FirstOrDefault();

                    if ((acesso != null) || (!string.IsNullOrEmpty(acesso.Programa)))
                    {
                        var usuario = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.Where(u => u.Id == IdUsuario).FirstOrDefault();

                        if ((usuario == null) || (!usuario.Ativo))
                        {
                            msg = "Usuário inexistente ou não ativo, não pode ser adicionado.";
                        }
                        else
                        {
                            //fazer add
                            var appResp = appServAcoesUsuarios.AddUsrAcesso(IdAcao, IdUsuario);
                            resposta = appResp.Execute;
                            msg = appResp.Message;
                            nome = usuario.Nome;
                            email = usuario.Email;
                        }
                    }
                    else
                    {
                        msg = string.Format("Acesso {0} não encontrado!", IdAcao);
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "Erro na solicitação: " + ex.Message;
            }

            var resultado = new
            {
                success = resposta,
                mensagem = msg,
                usuario = new
                {
                    Id  = IdUsuario,
                    Nome = nome,
                    Email = email
                }

            };

            return Json(resultado);
        }

        public JsonResult RemoveUsrAcao(long IdAcao, string IdUsuario)
        {
            bool resposta = false;
            string msg = string.Empty;

            try
            {
                using (AppServiceAcoesUsuarios appServiceAcoesUsuarios = new AppServiceAcoesUsuarios(this.UfwCartNew))
                {
                    var usrAcesso = appServiceAcoesUsuarios.UfwCartNew.Repositories.GenericRepository<UsuarioAcao>().GetWhere(u => (u.IdAcao == IdAcao) && (u.IdUsuario == IdUsuario)).FirstOrDefault();

                    if ((usrAcesso != null) || (!string.IsNullOrEmpty(usrAcesso.IdUsuario)))
                    {
                        //fazer remove
                        var appResp = appServiceAcoesUsuarios.RemoveUsrAcesso(IdAcao, IdUsuario);
                        resposta = appResp.Execute;
                        msg = appResp.Message;
                    }
                    else
                    {
                        msg = string.Format("Acesso {0} não encontrado para oo usuário!", IdAcao);
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "Erro na solicitação: " + ex.Message;
            }
            var resultado = new
            {
                success = resposta,
                mensagem = msg
            };

            return Json(resultado);
        }

        // GET: Acessos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Acessos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Acessos/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Acessos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Acessos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Acessos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Acessos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
