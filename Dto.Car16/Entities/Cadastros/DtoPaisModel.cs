using System;
using System.ComponentModel.DataAnnotations;
using Dto.Car16.Entities.Base;

namespace Dto.Car16.Entities.Cadastros
{
    public class DtoPaisModel : DtoEntityBaseModel
    {
        [Key]
        public override long Id { get; set; }

        [Display(Name = "Cod. IBGE")]
        public string CodIbge { get; set; }

        [Display(Name = "Sigla")]
        public string SiglaPais { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo Nome é Obrigatório", AllowEmptyStrings = false)]
        [MaxLength(100, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(5, ErrorMessage = "Mínimo {0} caracteres")]
        public string NomePais { get; set; }
    }
}
