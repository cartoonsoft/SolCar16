using Domain.Car16.Entities;
using Domain.Car16.Entities.Attributes;
using Domain.Car16.Enumeradores;
using Dto.Car16.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Dto.Car16.Entities.Cadastros
{
    public class DtoArquivoModeloDocxModel : DtoEntityBaseModel
    {
        [Key]
        public override long Id { get; set; }

        [Display(Name = "Nome Modelo")]
        [Required(ErrorMessage = "O campo nome do modelo é obrigatório",AllowEmptyStrings = false)]
        public string NomeModelo { get; set; }

        [Display(Name = "Tipo de ato")]
        [Required(ErrorMessage ="Selecione um tipo")]
        public int IdTipoAto { get; set; }

        //[Display(Name = "Tipo de Modelo")]
        //[Required(ErrorMessage = "Selecione algum tipo de modelo")]
        //public TipoArquivoModeloDocx NaturezaArquivoModeloDocx { get; set; }

        [Display(Name = "Arquivo")]
        [RequiredHttpPostedFileBase(ErrorMessage = "Selecione um arquivo.")]
        [IsWordFile(ErrorMessage = "O arquivo deve ser do tipo '.docx' ")]
        public List<HttpPostedFileBase> Files { get; set; }

        #region | Dados nao obrigatorios |

        [Display(Name = "Log Arquivo")]
        public LogArquivoModeloDocx LogArquivoModeloDocx { get; set; }

        [Display(Name = "Caminho e Arquivo")]
        public string Arquivo { get; set; }

        [Display(Name ="Bytes Arquivo")]
        public byte[] ArquivoByte { get; set; }

        [Display(Name ="Ativo")]
        public bool Ativo { get; set; }

        #endregion
    }
}
