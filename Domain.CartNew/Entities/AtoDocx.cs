﻿using System;
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
    public class AtoDocx: EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_ATO_DOCX")]  //ID_ATO_DOCX NUMERIC(19, 0)       not null,
        public override long? Id { get; set; }

        [Column("ID_CTA_ACESSO_SIST")] //ID_CTA_ACESSO_SIST NUMERIC(19, 0)       not null,
        public long IdCtaAcessoSist { get; set; }

        [Column("ID_ATO")] //ID_ATO NUMERIC(19, 0)       not null,
        public long IdAto { get; set; }

        [Column("ID_DOCX")] //ID_DOCX NUMERIC(19, 0)       not null,
        public long IdDocx { get; set; }

        [Column("IDX_PARA")] //IDX_PARA NUMERIC(5, 0),
        public short IdxParagrago { get; set; }

        [Column("TXT_HTML")] //TXT_HTML VARCHAR2(2048),
        public string TextoHtml { get; set; }
    }
}