using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Domain.CartNew.Attributes;
using Dto.CartNew.Base;
using Dto.CartNew.Entities.TodosCart;

namespace Dto.CartNew.Entities.Cart_11RI
{
    public class DtoModeloDoc : DtoEntityBaseModel
    {
        [Key]
        public override long? Id { get; set; }

        [Required(ErrorMessage = "Selecione cont de acesso", AllowEmptyStrings = false)]
        [Range(minimum: 1, maximum: long.MaxValue, ErrorMessage = "Erro IdCtaAcessoSist cdeve ser maior que zero")]
        public long IdCtaAcessoSist { get; set; }

        [Required(ErrorMessage = "Selecione algum tipo", AllowEmptyStrings = false)]
        public long IdTipoAto { get; set; }

        public string IdUsuarioCadastro { get; set; }

        public string IdUsuarioAlteracao { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        [Required(ErrorMessage = "O campo Descrição do modelo é obrigatório", AllowEmptyStrings = false)]
        public string Descricao { get; set; }

        [MaxLength(2048)]
        [StringLength(2048, ErrorMessage = "Máximo de {0} caracteres.")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Texto { get; set; }

        [MaxLength(512)]
        [StringLength(512, ErrorMessage = "Máximo de {0} caracteres.")]
        [DataType(DataType.MultilineText)]
        public string Orientacao { get; set; }

        public string UsuarioSistOperacional { get; set; }

        [Required]
        public bool Ativo { get; set; }


        public string IpLocal { get; set; }
    }
}
