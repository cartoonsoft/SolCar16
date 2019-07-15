using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Base;

namespace Domain.Cartorio.Entities.CartorioNew
{
    [Table("TB_ATO", Schema = "DEZESSEIS_NEW")]
    public class Ato: EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_ATO")]
        public override long? Id { get; set; }

        [Column("ID_CTA_ACESSO_SIST")] //ID_CTA_ACESSO_SIST NUMERIC(19, 0)       not null,
        public long IdContaAcessoSistema { get; set; }

        [Column("ID_TP_ATO")]
        public long IdTipoAto { get; set; }

        [Column("ID_PREMA")] //NUMERIC(19, 0),
        public long IdPrenotacao { get; set; }

        [Column("ID_USR_CAD")]
        public string IdUsuarioCadastro { get; set; }

        [Column("ID_USR_ALTER")]
        public string IdUsuarioAlteracao { get; set; }

        [Column("DT_CAD")]
        public DateTime DataCadastro { get; set; }

        [Column("DT_ALTER")]
        public DateTime? DataAlteracao { get; set; }

        [Column("NUM_SEQ_ATO")] //numeric(5,0)
        public short NumSequenciaAto { get; set; }

        [Column("DT_ATO")] //Date
        public string DataAto { get; set; }

        [Column("NRO_MATRICULA")]   //NRO_MATRICULA        VARCHAR2(20),
        public string NumMatricula { get; set; }
        
        [Column("ATIVO")] //NUMERIC(1,0)         default 0,
        public bool Ativo { get; set; }

        [Column("BLOQUEADO")] //NUMERIC(1,0),
        public bool Bloqueado { get; set; }

        [Column("OBSERCACAO")] // VARCHAR2(512),
        public string Observacao { get; set; }
    }
}
