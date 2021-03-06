﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartorio11RI.ViewModels
{
    public class AtoListViewModel
    {
        [Key]
        [Display(Name = "Id")]
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
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Última alteração")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? DataAlteracao { get; set; }

        [Required(ErrorMessage = "Número de matrícula é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Matrícula imóvel")]
        [MaxLength(19)]
        [StringLength(19, ErrorMessage = "Máximo de {0} caracteres.")]
        public string NumMatricula { get; set; }

        [Display(Name = "Registrado em")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DataRegPrenotacao { get; set; }  //data registro "R" da prenotacao onzeri.premad

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

        [MaxLength(512)]
        [StringLength(512, ErrorMessage = "Máximo de {0} caracteres.")]
        [Display(Name = "Observações")]
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }

        [Display(Name = "Status")]
        public string StatusAto { get; set; }

        [Display(Name = "Impressão ajustada", Description = "Minuta impressa corretamente e ajustada")]
        public bool ImpressaoAjustada { get; set; } //Status AI se uusari confirmou que o ajuste de impressao está ok   

        [Display(Name = "Conferido", Description = "Conferido texto e Ajuste de impressão")]
        public bool Conferido { get; set; } //Status CF   conferido ajuste impressoa e texto

        [Display(Name = "Gerado Ficha", Description = "Ficha já foi gerada (Docx)")]
        public bool GeradoFicha { get; set; }  //status GF (gerou docx)

        [Display(Name = "Salvo")]
        public bool Salvo { get; set; }  // CL (ato foi salvo)

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }  // CL (ao cancelar o Ativo = false)

        [Display(Name = "Finalizado", Description = "Ficha impressa e conferida novamente (Docx)")]
        public bool Finalizado { get; set; }  //status AF (gerou docx)

        [ScaffoldColumn(false)]
        public string IpLocal { get; set; }

        [Display(Name = "Ficha")]
        public short NumFicha { get; set; }  //numero da fihca informado pelo usuario

        [Display(Name = "Mat./Cód.")]
        public string Codigo
        {
            get { return this.NumMatricula.PadLeft(8, '0') + " - " + this.SiglaSeqAto + "/" + this.NumSequenciaAto.ToString(); }
        }

        public string SiglaSeqAto { get; set; }

        [Display(Name = "Nun. seq")]
        public short NumSequenciaAto { get; set; }

    }
}
