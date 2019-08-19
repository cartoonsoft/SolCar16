using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Domain.CartNew.Attributes;
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

        [Display(Name = "Nome Modelo")]
        [Required(ErrorMessage = "O campo nome do modelo é obrigatório", AllowEmptyStrings = false)]
        public string NomeModelo { get; set; }

        [Display(Name = "Arquivo")]
        [RequiredHttpPostedFileBase(ErrorMessage = "Selecione um arquivo.")]
        [IsWordFile(ErrorMessage = "O arquivo deve ser do tipo '.docx' ")]
        public List<HttpPostedFileBase> Files { get; set; }

        [Display(Name = "Caminho e Arquivo")]
        public string CaminhoEArquivo { get; set; }

        [Display(Name = "Bytes Arquivo")]
        public byte[] ArquivoByte { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        [Display(Name = "Log Arquivo")]
        public DtoLogModeloDocx LogArquivo { get; set; }
    }
}
