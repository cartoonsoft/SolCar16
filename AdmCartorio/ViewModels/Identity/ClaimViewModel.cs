﻿using System.ComponentModel.DataAnnotations;

namespace AdmCartorio.ViewModels.Identity
{
    public class ClaimViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Nome da Claim")]
        public string Type { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Valor da Claim")]
        public string Value { get; set; }
    }
}
