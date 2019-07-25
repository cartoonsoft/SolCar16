using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.Entities.Car16New
{
    [Table("TB_ST_VALOR_ANT", Schema = "DEZESSEIS_NEW")]
    public class StatusValorAnt  
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_ST_VALOR", Order = 0)]
        public  long IdStatusValor { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_ST_VALOR_ANT", Order = 1)]
        public long IdStatusValorAnt { get; set; }
    }
}
