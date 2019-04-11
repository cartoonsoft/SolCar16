﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Domain.Car16.Interfaces.UnitOfWork;
using Microsoft.AspNet.Identity;

namespace AdmCartorio.Controllers.Base
{
    public class AdmCartorioBaseController: Controller
    {
        private readonly IUnitOfWorkDataBseCar16 _unitOfWorkDataBseCar16;
        private readonly IUnitOfWorkDataBaseCar16New _unitOfWorkDataBseCar16New;

        public AdmCartorioBaseController(IUnitOfWorkDataBseCar16 unitOfWorkDataBseCar16, IUnitOfWorkDataBaseCar16New unitOfWorkDataBseCar16New)
        {
            _unitOfWorkDataBseCar16 = unitOfWorkDataBseCar16;
            _unitOfWorkDataBseCar16New = unitOfWorkDataBseCar16New;

            WindowsIdentity a = WindowsIdentity.GetCurrent();

            string userId = User.Identity.GetUserId();

        }

        protected IUnitOfWorkDataBseCar16 UnitOfWorkDataBseCar16
        {
            get { return _unitOfWorkDataBseCar16; }
        }

        protected IUnitOfWorkDataBaseCar16New UnitOfWorkDataBseCar16New
        {
            get { return _unitOfWorkDataBseCar16New; }

        }

    }
}