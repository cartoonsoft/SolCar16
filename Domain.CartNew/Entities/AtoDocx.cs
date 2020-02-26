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
    [Table("TB_ATO_DOCX", Schema = "DEZESSEIS_NEW")]
    public class AtoDocx
    {
        [Key]
        [Column("ID_ATO", Order = 0)] 
        public long IdAto { get; set; }

        [Key]
        [Column("ID_DOCX", Order = 1)] 
        public long IdDocx { get; set; }

        [Column("ID_CTA_ACESSO_SIST")] //ID_CTA_ACESSO_SIST NUMERIC(19, 0)       not null,
        public long IdCtaAcessoSist { get; set; }


        [Column("IDX_PARA")] //IDX_PARA NUMERIC(5, 0),
        public short IdxParagrago { get; set; }

        [Column("TXT_HTML")] //TXT_HTML VARCHAR2(2048),
        public string Texto { get; set; }
    }
}
