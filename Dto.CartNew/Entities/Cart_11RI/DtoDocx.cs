using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI
{
    public class DtoDocx
    {
        [Key]
        public long? Id { get; set; }

        public long IdContaAcessoSistema { get; set; }

        public string IdUsuarioCadastro { get; set; }

        public string IdUsuarioAlteracao { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string NumMatricula { get; set; }

        public short NumSequenciaFicha { get; set; }

        public DateTime DataDocx { get; set; }  //data cabeçalho docx

        public string NomeArquivoModelo { get; set; }

        public string NomeArquivo { get; set; }
    }
}
