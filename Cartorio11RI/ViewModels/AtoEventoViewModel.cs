using Domain.CartNew.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cartorio11RI.ViewModels
{
    public class AtoEventoViewModel
    {

        public AtoEventoViewModel()
        {
            this.TipoEvento = DataBaseOperacoes.undefined;
        }

        [Key]
        [ScaffoldColumn(false)]
        public long? Id { get; set; }

        [Required]
        [Display(Name = "Ato")]
        public long IdAto { get; set; }

        [Required]
        [Display(Name = "Tipo evento")]
        public DataBaseOperacoes TipoEvento { get; set; }

        [Required]
        [Display(Name = "Data")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime DataEvento { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public string IdUsuario { get; set; }

        [Required]
        [Display(Name = "Usuário")]
        public string NomeUsuario { get; set; }

        [Required]
        [Display(Name = "IP")]
        public string IP { get; set; }

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Status anterior")]
        public string StatusAnterior { get; set; }
    }
}