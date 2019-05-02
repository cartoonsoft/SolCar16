using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Car16.Entities.Cadastros
{
    public class DtoAto
    {
        public long? Id { get; set; }

        public long IdTipoAto { get; set; }

        public long IdPrenotacao { get; set; }

        public long IdContaAcessoSistema { get; set; }

        public long IdUsuarioCadastro { get; set; }


        public long? IdUsuarioAlteracao { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }


        public string NumMatricula { get; set; }


        public string NomeArquivo { get; set; }

        public byte[] ArquivoBytes { get; set; }

        public bool Ativo { get; set; }
    
        public bool Bloqueado { get; set; }

        public string Observacao { get; set; }
    }
}
