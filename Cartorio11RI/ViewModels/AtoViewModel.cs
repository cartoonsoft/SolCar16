using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.CartNew.Enumerations;

namespace Cartorio11RI.ViewModels
{
    public class AtoViewModel
    {
        public AtoViewModel()
        {
            this.Id = null;
            this.DataCadastro = DateTime.Now;
            this.DataAto = DateTime.Today;
            this.Pessoas = new List<PESSOAViewModel>();
            this.PREIMO = new PREIMOViewModel();
            this.Historico = new List<AtoEventoViewModel>();
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
        [Display(Name = "Modelo de documento")]
        public long IdModeloDoc { get; set; }

        [Required]
        [Display(Name = "Núm. prenotação")]
        public long IdPrenotacao { get; set; }

        [ScaffoldColumn(false)]
        public string IdUsuarioCadastro { get; set; }

        [ScaffoldColumn(false)]
        public string IdUsuarioAlteracao { get; set; }

        [Display(Name = "Cadastrado em")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [ReadOnly(true)]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Última alteração")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [ReadOnly(true)]
        public DateTime? DataAlteracao { get; set; }

        [Required(ErrorMessage = "Número de matrícula é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Matrícula imóvel")]
        [MaxLength(19)]
        [StringLength(19, ErrorMessage = "Máximo de {0} caracteres.")]
        public string NumMatricula { get; set; }

        [Display(Name = "Registrado em")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [ReadOnly(true)]
        public DateTime? DataRegPrenotacao{ get; set; }  //data registro "R" da prenotacao onzeri.premad

        [Display(Name = "Data do ato")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DataAto { get; set; }

        [Display(Name = "Descrição tipo ato")]
        public string DescricaoTipoAto { get; set; }

        [Required(ErrorMessage = "O campo Descrição do ato é obrigatório", AllowEmptyStrings = false)]
        [MaxLength(200)]
        [StringLength(200, ErrorMessage = "Máximo de {0} caracteres.")]
        [Display(Name = "Descrição do ato")]
        public string DescricaoAto { get; set; }

        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string TextoAnterior { get; set; }

        [MaxLength(2048)]
        [StringLength(2048, ErrorMessage = "Máximo de {0} caracteres.")]
        [Display(Name = "Texto")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Texto { get; set; }

        [MaxLength(512)]
        [StringLength(512, ErrorMessage = "Máximo de {0} caracteres.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Observações")]
        public string Observacao { get; set; }

        [Display(Name = "Status")]
        public string StatusAto { get; set; }

        [Display(Name = "Conferido Texto", Description = "Conferido texto do ato")]
        public bool ConfTexto { get; set; } //muda para Status CFT, fica readonly o texto, marcar como conferido o texto do ato, nomes, documentos, números,  gramatica e concordância.

        [Display(Name = "Conferido Docx", Description = "Conferido texto do ato")]
        public bool ConfDocx { get; set; } //muda para Status CFD, docx fica readonly, marcar como conferido o documneto (docx) gerado, alinhamento, fonte, texto, dist, topo, num. ficha, etc.

        [Display(Name = "Gerado Ficha", Description = "Ficha já foi gerada (Docx)")]
        public bool GeradoFicha { get; set; }  //status GF (gerou docx)

        [Display(Name = "Salvo")]
        public bool Salvo { get; set; }  // (ato foi salvo)
        
        [Display(Name = "Modo edição")]
        public bool PodeEditar { get; set; }  // campo que define se controles ckeditor e outros vão ficar readonly

        [Display(Name = "Finalizado", Description = "Ficha impressa e conferida novamente (Docx)")]
        public bool Finalizado { get; set; }  //status AF (antes deve: conferir texto, gerar docx e conferir docx)

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }  // CL (ao cancelar o Ativo = false)

        [ScaffoldColumn(false)]
        public string IpLocal { get; set; }

        [Required(ErrorMessage = "O campo Núm. Ficha é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Ficha")]
        [Range(1, 9999, ErrorMessage = "Número de Ficha inválid0!")]
        public short NumFicha { get; set; }  //numero da fihca informado pelo usuario

        [Required(ErrorMessage = "O campo Sigla do ato é obrigatório", AllowEmptyStrings = false)]
        [MaxLength(3)]
        [StringLength(3, ErrorMessage = "Máximo de {0} caracteres.")]
        [Display(Name = "Sigla")]
        public string SiglaSeqAto { get; set; }

        [Display(Name = "Nun. seq")]
        [Range(0, 9999, ErrorMessage = "Número de seqüência é inválido!")]
        public short NumSequenciaAto { get; set; }

        [Display(Name = "Distância")]
        [Range(0, 99, ErrorMessage = "Distância do topo (cm) é inválida!")]
        public short DistanciaTopo { get; set; } //distancia do inicio di texto do topa da pagina (cm)

        [Display(Name = "Frente/Verso")]
        public TipoFolhaFicha FolhaFicha { get; set; }

        public List<PESSOAViewModel> Pessoas { get; set; }

        public List<AtoEventoViewModel> Historico { get; set; }

        public PREIMOViewModel PREIMO { get; set; }

        public List<AtoPessoaViewModel> ListaAtoPessoa { get; set; }

    }
}