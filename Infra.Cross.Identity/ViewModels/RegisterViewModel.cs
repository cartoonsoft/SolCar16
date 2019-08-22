using Domain.CartNew.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Infra.Cross.Identity.ViewModels
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Nome completo")]
        public string Nome { get; set; }

        [Required]
        [MaxLength(20)]
        [StringLength(20, ErrorMessage = "M�nimo de {0} e m�ximo de {2} caracteres.", MinimumLength = 5)]
        [Display(Name = "Usu�rio")]
        public string UserName { get; set; }

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
        [StringLength(20, ErrorMessage = "M�nimo de {0} e m�ximo de {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "A senha de confirma��o est� diferente da anterior")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Telefone")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Ativo")]
        [Required]
        public bool Ativo { get; set; }

        [Display(Name = "Grupo")]
        [Required]
        public GrupoUsuarioEnum GrupoUsuario { get; set; }
    }
}