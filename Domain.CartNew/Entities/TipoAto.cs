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
    [Table("TB_TP_ATO", Schema = "DEZESSEIS_NEW")]
    public class TipoAto : EntityBase
    {
        [Key]
        [Column("ID_TP_ATO")]
        public override long? Id { get; set; }

        [Column("ID_CTA_ACESSO_SIST")]
        public long IdCtaAcessoSist { get; set; }

        [Column("ID_TP_ATO_PAI")]
        public long? IdTipoAtoPai { get; set; }

        [Column("DESCRICAO")]
        public string Descricao { get; set; }

        [Column("ORIENTACAO")]
        public string Orientacao { get; set; }

        [Column("SIGLA_SEQ_ATO")]
        public string SiglaSeqAto { get; set; }
    }
}
