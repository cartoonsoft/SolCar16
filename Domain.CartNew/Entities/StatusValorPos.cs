using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartNew.Entities
{
    [Table("TB_ST_VALOR_POST", Schema = "DEZESSEIS_NEW")]
    public class StatusValorPos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_ST_VALOR", Order = 0)]
        public long IdStatusValor { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_ST_VALOR_POS", Order = 1)]
        public long IdStatusValorPos { get; set; }
    }
}
