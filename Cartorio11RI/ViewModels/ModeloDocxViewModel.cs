using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Domain.CartNew.Attributes;

namespace Cartorio11RI.ViewModels
{
    public class ModeloDocxViewModel
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

        public string IdUsuarioCadastro { get; set; }

        public string IdUsuarioAlteracao { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        [Required(ErrorMessage = "O campo Descrição do modelo é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Descrição do modelo")]
        public string DescricaoModelo { get; set; }

        [Required(ErrorMessage = "O campo Tipo de ato do modelo é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Descrição tipo ato")]
        public string DescricaoTipoAto { get; set; }

        [RequiredHttpPostedFileBase(ErrorMessage = "Selecione um arquivo.")]
        [IsWordFile(ErrorMessage = "O arquivo deve ser do tipo '.docx' ")]
        [Display(Name = "Fazer upload do modelo")]
        public HttpPostedFileBase ArquivoDocxModelo { get; set; }

        public string CaminhoEArquivo { get; set; }

        [Required]
        public bool Ativo { get; set; }

        public string IpLocal { get; set; }

    }
}