using System.ComponentModel.DataAnnotations;

namespace Infra.Cross.Identity.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}