﻿using AdmCartorio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdmCartorio.Controllers
{
    public class AdmController : Controller
    {
        // GET: Adm
        public ActionResult AdmError(Exception excecao)
        {


            return View();
        }

        public ActionResult InternalServerError(Exception excecao)
        {
            InternalServerErrorViewModel internalServerErrorViewModel = new InternalServerErrorViewModel();
            internalServerErrorViewModel.Excecao = excecao;

            return View(internalServerErrorViewModel);
        }

    }
}