using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Base;

namespace Domain.Car16.Entities.Car16New
{
    [Table("TB_ST_VALOR", Schema = "DEZESSEIS_NEW")]
    public class StatusValor : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_ST_VALOR")]
        public override long? Id { get; set; }

        [Column("ID_STATUS")]
        public long IdStatus { get; set; }

        [Column("VALOR")]
        public string Valor { get; set; }

        [Column("DESC_PEQ")]
        public string DescricaoPequeno{ get; set; }

        [Column("DESCRICAO")]
        public string  Descricao { get; set; }

        [Column("FLAG_VAL_INI")]
        public bool FlagValIni { get; set; }  //status inicial

        [Column("FLAG_VAL_FIM")]              //status final
        public bool FlagValFim { get; set; }
    }
}
