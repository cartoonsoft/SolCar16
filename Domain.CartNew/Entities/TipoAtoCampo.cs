using Domain.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartNew.Entities
{
    [Table("TB_TP_ATO_CAMPO", Schema = "DEZESSEIS_NEW")]
    public class TipoAtoCampo
    {
        [Key]
        [Column("ID_TP_ATO", Order = 0)]
        public long IdTipoAto { get; set; }

        [Key]
        [Column("ID_CAMPO_TP_ATO", Order = 1)]
        public long IdCampoTipoAto { get; set; }
    }
}
