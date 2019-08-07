using System.ComponentModel.DataAnnotations;

namespace Infra.Cross.Identity.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Lembrar de min?")]
        public bool RememberMe { get; set; }
    }
}