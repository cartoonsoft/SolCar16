using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.CartNew.Interfaces.UnitOfWork;
using Cartorio11RI.Controllers.Base;
using AppServCart11RI.AppServices;
using Dto.CartNew.Entities.Cart_11RI.Diversos;

namespace Cartorio11RI.Controllers
{
    [Authorize]
    public class HomeController : CartorioBaseController
    {
        public HomeController(IUnitOfWorkDataBaseCartNew UfwCartNew = null) : base(UfwCartNew)
        {
            //

        }

        // GET: home/index
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Social()
        {
            return View();
        }

        // GET: home/inbox
        public ActionResult Inbox()
        {
            return View();
        }

        // GET: home/widgets
        public ActionResult Widgets()
        {
            return View();
        }

        // GET: home/chat
        public ActionResult Chat()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult MontarMenuUsuario(string IdUsuario)
        {
            IEnumerable<DtoMenu> Menu = new List<DtoMenu>();

            using (AppServiceAcoesUsuarios appService = new AppServiceAcoesUsuarios(this.UfwCartNew))
            {
                Menu = appService.ListaMenuUsusurio(IdUsuario);
            }

            return PartialView("_MenuUsuario", Menu);
        }
    }
}