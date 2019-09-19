using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cartorio11RI.ViewModels
{
    public class AtoViewModel
    {
        public AtoViewModel(long idCtaAcessoSist)
        {
            this.IdCtaAcessoSist = idCtaAcessoSist;
            this.Pessoas = new List<PESSOAViewModel>();
            this.PREIMO = new PREIMOViewModel();
        }

        [Key]
        [Display(Name = "Código")]
        [ScaffoldColumn(true)]
        public long? Id { get; set; }

        [ScaffoldColumn(false)]
        public long IdCtaAcessoSist { get; set; }

        [Display(Name = "Livro")]
        [ScaffoldColumn(true)]
        public long IdLivro { get; set; }

        [Display(Name = "Tipo de ato")]
        [ScaffoldColumn(true)]
        public long IdTipoAto { get; set; }

        [Display(Name = "Núm. prenotação")]
        [ScaffoldColumn(false)]
        public long IdPrenotacao { get; set; }

        [Display(Name = "Modelo de documento")]
        [ScaffoldColumn(true)]
        public long IdModeloDoc{ get; set; }

        [ScaffoldColumn(false)]
        public string IdUsuarioCadastro { get; set; }

        [ScaffoldColumn(false)]
        public string IdUsuarioAlteracao { get; set; }

        [Required]
        [Display(Name = "Cadastro")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Alteração")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DataAlteracao { get; set; }

        [Required]
        [Display(Name = "Nun. seq")]
        public short NumSequenciaAto { get; set; }

        [Display(Name = "Data do ato")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public string DataAto { get; set; }

        [Display(Name = "Matrícula imóvel")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public string NumMatricula { get; set; }

        [Required(ErrorMessage = "O campo Descrição do ato é obrigatório", AllowEmptyStrings = false)]
        [MaxLength(200)]
        [StringLength(200, ErrorMessage = "Máximo de {0} caracteres.")]
        [Display(Name = "Descrição do ato")]
        public string DescricaoAto { get; set; }

        [Display(Name = "Descrição tipo ato")]
        public string DescricaoTipoAto { get; set; }

        [MaxLength(512)]
        [StringLength(512, ErrorMessage = "Máximo de {0} caracteres.")]
        [Display(Name = "Observações")]
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        public string IpLocal { get; set; }

        public int IrParaFicha { get; set; }

        public bool IrParaVerso { get; set; }

        public bool ExisteNoSistema { get; set; }

        public float QuantidadeCentimetrosDaBorda { get; set; }

        public List<PESSOAViewModel> Pessoas { get; set; }

        public PREIMOViewModel PREIMO { get; set; }
    }
}