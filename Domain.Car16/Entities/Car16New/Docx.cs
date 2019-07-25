using Domain.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.Entities.Car16New
{
    [Table("TB_DOCX", Schema = "DEZESSEIS_NEW")]
    public class Docx : EntityBase
    {
        [Column("ID_DOCX")] //ID_DOCX NUMERIC(38, 0)       not null,
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override long? Id { get; set; }

        [Column("ID_CTA_ACESSO_SIST")] //ID_CTA_ACESSO_SIST NUMERIC(38, 0)       not null,
        public long IdContaAcessoSistema { get; set; }

        [Column("ID_USR_CAD")]
        public string IdUsuarioCadastro { get; set; }

        [Column("ID_USR_ALTER")]
        public string IdUsuarioAlteracao { get; set; }

        [Column("DT_CAD")]
        public DateTime DataCadastro { get; set; }

        [Column("DT_ALTER")]
        public DateTime? DataAlteracao { get; set; }

        [Column("NRO_MATRICULA")]   //NRO_MATRICULA        VARCHAR2(20),
        public string NumMatricula { get; set; }

        [Column("NUM_SEQ_FICHA")]   //      NUM_SEQ_FICHA SMALLINT,
        public short Ficha { get; set; }

        [Column("DT_DOCX")]    //DT_DOCX              DATE,
        public DateTime DataDocx { get; set; }

        [Column("NOM_ARQ_MOD")] //NOM_ARQ_MOD VARCHAR2(400),
        public string NomeArqModelo { get; set; }

        [Column("NOM_ARQ")] //NOM_ARQ VARCHAR2(400),
        public string NomeArq { get; set; }
    }
}
