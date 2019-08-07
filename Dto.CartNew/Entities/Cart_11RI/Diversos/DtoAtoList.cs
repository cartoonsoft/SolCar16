using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI.Diversos
{
    public class DtoAtoList
    {
        [Key]
        public long? Id { get; set; }

        public long IdTipoAto { get; set; }

        public long IdPrenotacao { get; set; }

        public long IdContaAcessoSistema { get; set; }

        public string DescricaoTipoAto { get; set; }
        public string Codigo { get; set; }

        public string IdUsuarioCadastro { get; set; }

        public string IdUsuarioAlteracao { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string NumMatricula { get; set; }

        public string NomeArquivo { get; set; }

        public bool Ativo { get; set; }

        public bool Bloqueado { get; set; }

        public string Observacao { get; set; }

        public long NumSequencia { get; set; }
    }
}
