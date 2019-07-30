using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Base;

namespace Domain.CartNew.Entities
{
    [Table("TB_TP_CFG", Schema = "DEZESSEIS_NEW")]
    public class TipoCfgCtaAcessoSist : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_TP_CFG")]
        public override long? Id { get; set; }

        [Column("SIGLA")]
        public string Sigla { get; set; }

        [Column("DESCRICAO")]
        public string Descricao { get; set; }
    }
}
