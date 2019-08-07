using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.CartNew.Interfaces.UnitOfWork;
using AdmCartorio.Controllers.Base;

namespace AdmCartorio.Controllers
{
    [Authorize]
    public class HomeController : CartorioBaseController
    {
        #region | Construtores |
        public HomeController() : base(null)
        {
            //

        }

        public HomeController(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            //
        }
        #endregion

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
        public ActionResult MontarMenu(string IdUsuario)
        { 


            return PartialView();
        }
    }
}