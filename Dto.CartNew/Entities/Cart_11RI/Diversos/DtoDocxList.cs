using Dto.CartNew.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI
{
    public class DtoDocxList 
    {
        public long IdAto { get; set; }

        public long IdDocx { get; set; }

        public long IdCtaAcessoSist { get; set; }

        public long IdTipoAto { get; set; }

        public long IdPrenotacao { get; set; }

        public long IdxParagrafo { get; set; }

        public string IdUsuarioCadastroDocx { get; set; }

        public string IdUsuarioAlteracaoDocx { get; set; }

        public DateTime DataCadastroDocx { get; set; }

        public DateTime? DataAlteracaoDocx { get; set; }

        public short NumSequenciaFicha { get; set; }

        public DateTime DataDocx { get; set; }  //data cabeçalho docx

        public string NomeArquivo { get; set; }

        public string TextoHtml { get; set; }

        public string  NumMatricula { get; set; }

        public string  DescricaoAto { get; set; }

        public string StatusAto { get; set; }

        public string ObsAto  { get; set; }
    }
}
