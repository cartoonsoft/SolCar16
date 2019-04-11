using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AdmCartorio.ViewModels.Identity
{
    public class AccountLoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Lembrar login?")]
        public bool RememberMe { get; set; }
    }

    public class AccountExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }
    }

    public class AccountExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class AccountSendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }

        [HiddenInput]
        public string UserId { get; set; }
    }

    public class AccountVerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Lembrar este Browser?")]
        public bool RememberBrowser { get; set; }

        [HiddenInput]
        public string UserId { get; set; }

    }

    public class AccountForgotViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }

    public class AccountRegisterViewModel
    {
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Usuário")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [EmailAddress]
        [System.ComponentModel.DataAnnotations.Compare("Email")]
        [Display(Name = "Confirme seu e-mail")]
        public string EmailConfirm { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [Display(Name = "Confirme sua senha")]
        public string PasswordConfirm { get; set; }
    }

    public class AccountResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "As senhas não se coincidem.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class AccountForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}