using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Domain.CartNew.Attributes;
using System.ComponentModel;
using System.Web.Mvc;

namespace Cartorio11RI.ViewModels
{
    public class ModeloDocViewModel
    {
        [Display(Name = "Código")]
        public long? Id { get; set; }

        [Required(ErrorMessage = "Selecione cont de acesso", AllowEmptyStrings = false)]
        [Display(Name = "Conta acesso")]
        [Range(minimum: 1, maximum: long.MaxValue, ErrorMessage = "Erro IdCtaAcessoSist cdeve ser maior que zero")]
        public long IdCtaAcessoSist { get; set; }

        [Required(ErrorMessage = "Selecione algum tipo", AllowEmptyStrings = false)]
        [Display(Name = "Tipo de ato")]
        public long IdTipoAto { get; set; }

        [Display(Name = "Campo")]
        public long IdCampoTipoAto { get; set; }

        public string IdUsuarioCadastro { get; set; }

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

        [Required(ErrorMessage = "O campo Descrição do modelo é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Descrição do modelo")]
        [MaxLength(200)]
        [StringLength(200, ErrorMessage = "Máximo de {0} caracteres.")]
        public string Descricao { get; set; }

        [MaxLength(2048)]
        [StringLength(2048, ErrorMessage = "Máximo de {0} caracteres.")]
        [Display(Name = "Texto")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Texto { get; set; }

        [MaxLength(512)]
        [StringLength(512, ErrorMessage = "Máximo de {0} caracteres.")]
        [Display(Name = "Orientações")]
        [DataType(DataType.MultilineText)]
        public string Orientacao { get; set; }

        public string UsuarioSistOperacional { get; set; }

        public bool Ativo { get; set; }

        public string IpLocal { get; set; }

    }
}