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
    [Table("TB_DOCX", Schema = "DEZESSEIS_NEW")]
    public class Docx : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_DOCX")] 
        public override long? Id { get; set; }

        [Column("ID_CTA_ACESSO_SIST")] //ID_CTA_ACESSO_SIST NUMERIC(38, 0)       not null,
        public long IdCtaAcessoSist { get; set; }

        [Column("ID_USR_CAD")]
        public string IdUsuarioCadastro { get; set; }

        [Column("ID_USR_ALTER")]
        public string IdUsuarioAlteracao { get; set; }

        [Column("DT_CAD")]
        public DateTime DataCadastro { get; set; }

        [Column("DT_ALTER")]
        public DateTime? DataAlteracao { get; set; }

        [Column("NUM_FICHA")]   //      NUM_SEQ_FICHA SMALLINT,
        public short NumFicha { get; set; }

        [Column("DT_DOCX")]    //DT_DOCX              DATE,
        public DateTime DataDocx { get; set; }

        [Column("NOM_ARQ")] //NOM_ARQ VARCHAR2(400),
        public string NomeArq { get; set; }
    }
}
