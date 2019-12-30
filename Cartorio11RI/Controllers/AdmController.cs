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
            Exception ex = TempData["excecaoGerada"] as Exception;

            InternalServerErrorViewModel internalServerError = new InternalServerErrorViewModel();
            internalServerError.Descricao = descricao;
            internalServerError.Excecao = ex;

            return View(internalServerError);
        }


        public ActionResult BusinessError(string descricao)
        {
            BusinessErrorViewModel businessError = TempData["businessError"] as BusinessErrorViewModel;
            Exception ex = TempData["excecaoGerada"] as Exception;
            businessError.Descricao = descricao;
            businessError.Excecao = ex;

            return View(businessError);
        }

        public ActionResult Error404()
        {

            return View();
        }

    }
}
