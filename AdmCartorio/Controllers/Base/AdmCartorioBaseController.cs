using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using AdmCartorio.App_Start.Identity;
using AdmCartorio.Models.Identity.Entities;
using Domain.Cartorio.enums;
using Domain.Cartorio.Interfaces.UnitOfWork;
using Infra.Data.Cartorio.UnitsOfWork;
using Infra.Data.Cartorio.UnitsOfWork.DbCartorio;
using Infra.Data.Cartorio.UnitsOfWork.DbCartorioNew;

namespace AdmCartorio.Controllers.Base
{
    public class AdmCartorioBaseController : Controller
    {
        private readonly UnitOfWorkDataBaseCartorio _UnitOfWorkDataBaseCartorio;
        private readonly UnitOfWorkDataBaseCartorioNew _UnitOfWorkDataBaseCartorioNew;
        private readonly ApplicationUser _currentUser;

        public AdmCartorioBaseController(IUnitOfWorkDataBaseCartorio UnitOfWorkDataBaseCartorio, IUnitOfWorkDataBaseCartorioNew UnitOfWorkDataBaseCartorioNew)
        {
            _UnitOfWorkDataBaseCartorio = new UnitOfWorkDataBaseCartorio(BaseDados.DesenvDezesseis);
            _UnitOfWorkDataBaseCartorioNew = new UnitOfWorkDataBaseCartorioNew(BaseDados.DesenvDezesseisNew);
            this._currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            //WindowsIdentity a = WindowsIdentity.GetCurrent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_UnitOfWorkDataBaseCartorio != null) {
                    _UnitOfWorkDataBaseCartorio.Dispose();
                }

                if (_UnitOfWorkDataBaseCartorioNew != null)
                {
                    _UnitOfWorkDataBaseCartorioNew.Dispose();
                }
            }
            base.Dispose(disposing);

        }

        protected ApplicationUser UsuarioAtual {
            get { return _currentUser; }
        } 

        protected IUnitOfWorkDataBaseCartorio UnitOfWorkDataBaseCartorio
        {
            get { return _UnitOfWorkDataBaseCartorio; }
        }

        protected IUnitOfWorkDataBaseCartorioNew UnitOfWorkDataBaseCartorioNew
        {
            get { return _UnitOfWorkDataBaseCartorioNew; }

        }

    }
}