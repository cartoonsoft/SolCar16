using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cartorio11RI.ViewModels
{
    public class AtoViewModel
    {
        [Key]
        [Display(Name = "Código")]
        [ScaffoldColumn(true)]
        public long? Id { get; set; }

        [ScaffoldColumn(false)]
        public long IdCtaAcessoSist { get; set; }

        [ScaffoldColumn(false)]
        public long IdTipoAto { get; set; }

        [ScaffoldColumn(false)]
        public long IdPrenotacao { get; set; }

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

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        [Display(Name = "Bloqueado")]
        public bool Bloqueado { get; set; }

        [MaxLength(512)]
        [StringLength(512, ErrorMessage = "Máximo de {0} caracteres.")]
        [Display(Name = "Usuário")]
        [DataType(DataType.Text)]
        public string Observacao { get; set; }

        public PREIMOViewModel PREIMO { get; set; }

        public List<PESSOAViewModel> Pessoas { get; set; }

        public ModeloDocxSimplificadoViewModel ArquivoModelo { get; set; }

        public string TipoPessoa { get; set; }

        public int IrParaFicha { get; set; }

        public bool IrParaVerso { get; set; }

        public bool ExisteNoSistema { get; set; }

        public float QuantidadeCentimetrosDaBorda { get; set; }
    }
}