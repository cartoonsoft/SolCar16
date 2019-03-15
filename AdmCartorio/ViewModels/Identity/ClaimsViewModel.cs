using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdmCartorio.ViewModels.Identity
{
    [Table("AspNetClaims")]
    public class ClaimsViewModel
    {
        public ClaimsViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Fornce�a um nome para a Claim")]
        [MaxLength(128, ErrorMessage = "Tamanho m�ximo {0} excedido")]
        [Display(Name = "Nome da Claim")]
        public string Name { get; set; }
    }
}