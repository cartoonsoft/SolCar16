﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cartorio11RI.ViewModels
{
    public class InternalServerErrorViewModel: BaseErrorViewModel
    {
        [Display(Name = "Action")]
        public string Action { get; set; }

        [Display(Name = "Controller")]
        public string Controller { get; set; }
    }
}