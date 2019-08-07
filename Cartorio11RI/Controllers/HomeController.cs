using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.CartNew.Interfaces.UnitOfWork;
using Cartorio11RI.Controllers.Base;

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
        public ActionResult MontarMenu(string IdUsuario)
        { 


            return PartialView();
        }
    }
}