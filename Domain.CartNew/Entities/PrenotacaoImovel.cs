using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartNew.Entities
{
    [Table("TB_PRE_IMO", Schema = "DEZESSEIS_NEW")]
    public class PrenotacaoImovel
    {
        [Key]
        [Column("ID_PREMA", Order = 0)]
        public long IdPrenotacao { get; set; }

        [Key]
        [Column("NRO_MATRICULA", Order = 1)]
        public string NumMatricula { get; set; }

        [Column("ID_USUARIO")]
        public string IdUsuario { get; set; }

    }
}
