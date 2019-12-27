using Cartorio11RI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cartorio11RI.Controllers
{
    public class AdmController : Controller
    {
        // GET: Adm
        public ActionResult AdmError(Exception excecao)
        {
            return View();
        }

        public ActionResult InternalServerError(string descricao)
        {
            Exception ex = TempData["error"] as Exception;

            InternalServerErrorViewModel internalServerError = new InternalServerErrorViewModel();
            internalServerError.Data = DateTime.Now;
            internalServerError.Descricao = descricao;
            internalServerError.Excecao = ex;

            return View(internalServerError);
        }


        public ActionResult BusinessError(string descricao)
        {
            //Exception ex = TempData["error"] as Exception;
            BusinessErrorViewModel businessError = TempData["businessError"] as BusinessErrorViewModel;

            return View(businessError);
        }

        public ActionResult Error404()
        {

            return View();
        }

    }
}
