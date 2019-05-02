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
using Domain.Car16.enums;
using Domain.Car16.Interfaces.UnitOfWork;
using Infra.Data.Car16.UnitsOfWork;

namespace AdmCartorio.Controllers.Base
{
    public class AdmCartorioBaseController : Controller
    {
        private readonly UnitOfWorkDataBaseCar16 _unitOfWorkDataBaseCar16;
        private readonly UnitOfWorkDataBaseCar16New _unitOfWorkDataBseCar16New;
        private readonly ApplicationUser _currentUser;

        public AdmCartorioBaseController(IUnitOfWorkDataBaseCar16 unitOfWorkDataBaseCar16, IUnitOfWorkDataBaseCar16New unitOfWorkDataBseCar16New)
        {
            _unitOfWorkDataBaseCar16 = new UnitOfWorkDataBaseCar16(BaseDados.DesenvDezesseis);
            _unitOfWorkDataBseCar16New = new UnitOfWorkDataBaseCar16New(BaseDados.DesenvDezesseisNew);
            this._currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            //WindowsIdentity a = WindowsIdentity.GetCurrent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_unitOfWorkDataBaseCar16 != null) {
                    _unitOfWorkDataBaseCar16.Dispose();
                }

                if (_unitOfWorkDataBseCar16New != null)
                {
                    _unitOfWorkDataBseCar16New.Dispose();
                }
            }
            base.Dispose(disposing);

        }

        protected ApplicationUser UsuarioAtual {
            get { return _currentUser; }
        } 

        protected IUnitOfWorkDataBaseCar16 UnitOfWorkDataBaseCar16
        {
            get { return _unitOfWorkDataBaseCar16; }
        }

        protected IUnitOfWorkDataBaseCar16New UnitOfWorkDataBaseCar16New
        {
            get { return _unitOfWorkDataBseCar16New; }

        }

    }
}