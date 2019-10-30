using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.UnitOfWork;
using Cartorio11RI.Controllers.Base;
using Cartorio11RI.ViewModels;
using AppServCart11RI.AppServices;
using Infra.Cross.Identity.Models;
using Infra.Cross.Identity.Configuration;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace Cartorio11RI.Controllers
{
    [Authorize]
    public class AcoesController : CartorioBaseController
    {
        private long[] acoesAdmin = { 83, 84 };  //ids de açoes que so pode ser concedidas à usuários admin

        public AcoesController(IUnitOfWorkDataBaseCartNew UfwCartNew = null) : base(UfwCartNew)
        {
            //
            
        }

        private void AddListaAcao(AppServiceAcoesUsuarios appServAcoesUsuarios, List<ApplicationUser> listaUsrSist, List<Acao> listaTodasAcoes, List<AcaoViewModel> listaAcoes)
        {
            bool addUser;
            foreach (var acao in listaTodasAcoes)
            {
                List<UsuarioAcaoViewModel> listaUsrAcao = new List<UsuarioAcaoViewModel>();
                var listaUsers = appServAcoesUsuarios.UfwCartNew.Repositories.GenericRepository<UsuarioAcao>().GetWhere(u => u.IdAcao == acao.Id).ToList();

                foreach (var usuario in listaUsers)
                {
                    addUser = true;
                    var usrtmp = listaUsrSist.Find(u => u.Id == usuario.IdUsuario);

                    if (acoesAdmin.Contains(acao.Id??0))
                    {
                        addUser = usrtmp.Claims.Where(c => (c.ClaimType == ClaimTypes.Role) && (c.ClaimValue == "Admin")).Count() > 0;
                    }

                    if ((usrtmp != null) && addUser)
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

                listaAcoes.Add(new AcaoViewModel
                {
                    Id = acao.Id,
                    IdCtaAcessoSist = acao.IdCtaAcessoSist,
                    SeqAcesso = acao.SeqAcesso,
                    Programa = acao.Programa,
                    Obs = acao.Obs,
                    DescricaoPequeno = acao.DescricaoPequeno,
                    DescricaoMedio = acao.DescricaoMedio,
                    DescricaoGrande = acao.DescricaoGrande,
                    DescricaoTip = acao.DescricaoTip,
                    DescricaoBalao = acao.DescricaoBalao,
                    Orientacao = acao.Orientacao,
                    Action = acao.Action,
                    Controller = acao.Controller,
                    Parametros = acao.Parametros,
                    IconeWeb = acao.IconeWeb,
                    IconeMobile = acao.IconeMobile,
                    Ativo = acao.Ativo,
                    EmManutencao = acao.EmManutencao,
                    ListaUsersAcao = listaUsrAcao
                });
            }
        }

        // GET: Acoes
        public ActionResult IndexAcao()
        {
            List<ApplicationUser> listaUsrSist = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.Where(u => u.Ativo == true).OrderBy(u => u.UserName).ToList();
            List<AcaoViewModel> listaAcoes  = new List<AcaoViewModel>();

            using (AppServiceAcoesUsuarios appServAcoesUsuarios = new AppServiceAcoesUsuarios(this.UfwCartNew, this.IdCtaAcessoSist))
            {
                IEnumerable<Acao> listaTodasAcoes = appServAcoesUsuarios.UfwCartNew.Repositories.GenericRepository<Acao>().Get().OrderBy(a => a.DescricaoMedio).ToList();

                AddListaAcao(appServAcoesUsuarios, listaUsrSist, listaTodasAcoes.Where(a => a.SeqAcesso == null).ToList(), listaAcoes);
                AddListaAcao(appServAcoesUsuarios, listaUsrSist, listaTodasAcoes.Where(a => a.SeqAcesso != null).ToList(), listaAcoes);
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
            string usrName = string.Empty;
            string email = string.Empty;

            try
            {
                using (AppServiceAcoesUsuarios appServAcoesUsuarios = new AppServiceAcoesUsuarios(this.UfwCartNew, this.IdCtaAcessoSist))
                {
                    Acao acao = appServAcoesUsuarios.UfwCartNew.Repositories.GenericRepository<Acao>().GetWhere(a => a.Id == IdAcao).FirstOrDefault();

                    if ((acao != null) || (!string.IsNullOrEmpty(acao.Programa)))
                    {
                        var usuario = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.Where(u => u.Id == IdUsuario).FirstOrDefault();

                        if ((usuario == null) || (!usuario.Ativo))
                        {
                            msg = "Usuário inexistente ou não ativo, não pode ser adicionado.";
                        }
                        else
                        {
                            if (acoesAdmin.Contains(acao.Id ?? 0))
                            {
                                if (!(usuario.Claims.Where(c => (c.ClaimType == ClaimTypes.Role) && (c.ClaimValue == "Admin")).Count() > 0))
                                {
                                    msg = "Esta permissão só pode ser atribuída para usuários do grupo Admin!";
                                }
                            }
                            else
                            {
                                //fazer add
                                var appResp = appServAcoesUsuarios.AddUsrAcao(IdAcao, IdUsuario);
                                resposta = appResp.Execute;
                                msg = appResp.Message;
                                usrName = usuario.UserName;
                                nome = usuario.Nome;
                                email = usuario.Email;
                            }
                        }
                    }
                    else
                    {
                        msg = string.Format("Ação {0} não encontrado!", IdAcao);
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "Erro na solicitação: " + ex.Message;
            }

            var resultado = new
            {
                resposta = resposta,
                msg = msg,
                usuario = new
                {
                    Id  = IdUsuario,
                    UserName = usrName,
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
                using (AppServiceAcoesUsuarios appServiceAcoesUsuarios = new AppServiceAcoesUsuarios(this.UfwCartNew, this.IdCtaAcessoSist))
                {
                    var usrAcao = appServiceAcoesUsuarios.UfwCartNew.Repositories.GenericRepository<UsuarioAcao>().GetWhere(u => (u.IdAcao == IdAcao) && (u.IdUsuario == IdUsuario)).FirstOrDefault();

                    if ((usrAcao != null) || (!string.IsNullOrEmpty(usrAcao.IdUsuario)))
                    {
                        //fazer remove
                        var appResp = appServiceAcoesUsuarios.RemoveUsrAcao(IdAcao, IdUsuario);
                        resposta = appResp.Execute;
                        msg = appResp.Message;
                    }
                    else
                    {
                        msg = string.Format("Ação {0} não encontrado para o usuário!", IdAcao);
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "Erro na solicitação: " + ex.Message;
            }
            var resultado = new
            {
                resposta = resposta,
                msg = msg
            };

            return Json(resultado);
        }

    }
}
