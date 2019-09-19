using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Domain.CartNew.Attributes;
using Dto.CartNew.Base;
using Dto.CartNew.Entities.TodosCart;

namespace Dto.CartNew.Entities.Cart_11RI
{
    public class DtoModeloDocx : DtoEntityBaseModel
    {
        [Key]
        public override long? Id { get; set; }

        public long IdCtaAcessoSist { get; set; }

        [Display(Name = "Tipo de ato")]
        [Required(ErrorMessage = "Selecione um tipo")]
        public long IdTipoAto { get; set; }

        public string IdUsuarioCadastro { get; set; }

        public string IdUsuarioAlteracao { get; set; }

        public DateTime DataCadastro { get; }

        public DateTime? DataAlteracao { get; }

        [Display(Name = "Descrição do modelo")]
        [Required(ErrorMessage = "O campo Descrição do modelo é obrigatório", AllowEmptyStrings = false)]
        public string DescricaoModelo { get; set; }

        [Display(Name = "Orientações")]
        public string Orientacao { get; set; }

        [Display(Name = "Arquivo")]
        [RequiredHttpPostedFileBase(ErrorMessage = "Selecione um arquivo.")]
        [IsWordFile(ErrorMessage = "O arquivo deve ser do tipo '.docx' ")]
        public HttpPostedFileBase ArquivoDocxModelo { get; set; }

        [Display(Name = "Caminho e Arquivo")]
        public string CaminhoEArquivo { get; set; }

        [Display(Name = "Bytes Arquivo")]
        public byte[] ArquivoByte { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        public string UsuarioSistOperacional { get; set; }

        public string IpLocal { get; set; }
    }
}
