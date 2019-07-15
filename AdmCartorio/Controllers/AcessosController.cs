using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AdmCartorio.Controllers.Base;
using AdmCartorio.ViewModels;
using AppServices.Cartorio.AppServices;
using Domain.Cartorio.Entities.Cartorio;
using AdmCartorio.Models.Identity.Entities;
using AdmCartorio.App_Start.Identity;
using Domain.Car16.Entities.Car16New;

namespace AdmCartorio.Controllers
{
    public class AcessosController : AdmCartorioBaseController
    {
        public AcessosController(): base(null, null)
        {
            //
        }

        // GET: Acessos
        public ActionResult Index()
        {
            List<ApplicationUser> listaUsrSist = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.Where(u => u.Ativo == true).ToList();
            List<ACESSOViewModel> listAcessos = new List<ACESSOViewModel>();

            using (AppServiceAcesso appServAcesso = new AppServiceAcesso(this.UnitOfWorkDataBaseCartorio, this.UnitOfWorkDataBaseCartorioNew))
            {
                IEnumerable<ACESSO> listaAcessos = appServAcesso.UfwCart.Repositories.GenericRepository<ACESSO>().GetAll().OrderBy(o => o.OBS);
                foreach (var acesso in listaAcessos) 
                {
                    List<UsuarioAcessoViewModel> listaUsersAcessos = new List<UsuarioAcessoViewModel>();
                    var listaUsers = appServAcesso.UfwCartNew.Repositories.GenericRepository<UsuarioAcesso>().GetWhere(u => u.SeqAcesso == acesso.SEQACESSO).ToList();
                    foreach (var usuario in listaUsers)
                    {
                        var usrtmp = listaUsrSist.Find(u => u.Id == usuario.IdUsuario);

                        listaUsersAcessos.Add(new UsuarioAcessoViewModel
                        {
                            IdUsuario = usuario.IdUsuario,
                            IdContaAcessoSistema = usuario.IdContaAcessoSistema,
                            SeqAcesso = usuario.SeqAcesso,
                            UserName = usrtmp.UserName,
                            Email = usrtmp.Email,
                            Nome = usrtmp.Nome
                        }); 

                    }

                    listAcessos.Add(new ACESSOViewModel {
                        SEQACESSO = acesso.SEQACESSO,
                        PROGRAMA = acesso.PROGRAMA,
                        OBS = acesso.OBS,
                        ListaUsersAcesso = listaUsersAcessos
                    });
                }

                //listAcessos = Mapper.Map<IEnumerable<DtoAcesso>, IEnumerable<ACESSOViewModel>>(null);
            }

            ViewBag.listaUsuarios = new SelectList(listaUsrSist, "Id", "Nome");

            return View(listAcessos);
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
                // TODO: Add insert logic here

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
                // TODO: Add update logic here

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
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
