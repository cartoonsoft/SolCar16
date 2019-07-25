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
    [Table("TB_TP_MENU", Schema = "DEZESSEIS_NEW")]
    public class TipoMenu : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_TP_MENU")]
        public override long? Id { get; set; }

        [Column("DESCRICAO")]
        public string Descricao { get; set; }

        [Column("ATIVO")]
        public bool Ativo { get; set; }
    }
}
