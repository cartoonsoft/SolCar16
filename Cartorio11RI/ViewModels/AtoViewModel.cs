using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cartorio11RI.ViewModels
{
    public class AtoViewModel
    {
        public AtoViewModel()
        {
            this.DataCadastro = DateTime.Today;
            this.DataAto = DateTime.Today;
            this.Pessoas = new List<PESSOAViewModel>();
            this.PREIMO = new PREIMOViewModel();
        }

        [Key]
        [Display(Name = "Código")]
        public long? Id { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public long IdCtaAcessoSist { get; set; }

        [Required]
        [Display(Name = "Livro")]
        public long IdLivro { get; set; }

        [Required]
        [Display(Name = "Tipo de ato")]
        public long IdTipoAto { get; set; }

        [Required]
        [Display(Name = "Núm. prenotação")]
        public long IdPrenotacao { get; set; }

        [Required(ErrorMessage = "Número de matrícula é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Matrícula imóvel")]
        [MaxLength(19)]
        [StringLength(19, ErrorMessage = "Máximo de {0} caracteres.")]
        public string NumMatricula { get; set; }

        [Required]
        [Display(Name = "Modelo de documento")]
        public long IdModeloDoc{ get; set; }

        [ScaffoldColumn(false)]
        public string IdUsuarioCadastro { get; set; }

        [ScaffoldColumn(false)]
        public string IdUsuarioAlteracao { get; set; }

        [Required]
        [Display(Name = "Cadastro")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Alteração")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DataAlteracao { get; set; }

        [Required]
        [Display(Name = "Nun. seq")]
        public short NumSequenciaAto { get; set; }

        [Display(Name = "Data do ato")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DataAto { get; set; }

        [Required(ErrorMessage = "O campo Descrição do ato é obrigatório", AllowEmptyStrings = false)]
        [MaxLength(200)]
        [StringLength(200, ErrorMessage = "Máximo de {0} caracteres.")]
        [Display(Name = "Descrição do ato")]
        public string DescricaoAto { get; set; }

        [Display(Name = "Descrição tipo ato")]
        public string DescricaoTipoAto { get; set; }

        [Display(Name = "Texto")]
        [DataType(DataType.Text)]
        [AllowHtml]
        public string Texto { get; set; }

        [MaxLength(512)]
        [StringLength(512, ErrorMessage = "Máximo de {0} caracteres.")]
        [Display(Name = "Observações")]
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }

        [Display(Name = "Status ato")]
        public string StatusAto { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        [ScaffoldColumn(false)]
        public string IpLocal { get; set; }

        [Display(Name = "Ficha número")]
        public short NumFicha { get; set; }  //numero da fihca informado pelo usuario

        [Display(Name = "Distância topo (cm)")]
        [Range(0, 99.99, ErrorMessage = "Distância do topo (cm) é inválida!")]
        public decimal TextoDistanciaTopo { get; set; } //distancia do inicio di texto do topa da pagina (cm)

        [ScaffoldColumn(false)]
        public bool DocxGerado { get; set; }

        public List<PESSOAViewModel> Pessoas { get; set; }

        public PREIMOViewModel PREIMO { get; set; }
    }
}