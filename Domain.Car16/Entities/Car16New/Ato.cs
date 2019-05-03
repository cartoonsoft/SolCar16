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
    [Table("TB_ATO")]
    public class Ato: EntityBase
    {
        [Key]
        [Column("ID_ATO")]
        public override long? Id { get; set; }

        [Column("ID_TP_ATO")]
        public long IdTipoAto { get; set; }

        [Column("ID_PREMA")] //NUMERIC(38, 0),
        public long IdPrenotacao { get; set; }

        [Column("ID_CTA_ACESSO_SIST")]
        public long IdContaAcessoSistema { get; set; }

        [Column("ID_USR_CAD")]
        public long IdUsuarioCadastro { get; set; }

        [Column("ID_USR_ALTER")]
        public long? IdUsuarioAlteracao { get; set; }

        [Column("DT_CAD")]
        public DateTime DataCadastro { get; set; }

        [Column("DT_ALTER")]
        public DateTime? DataAlteracao { get; set; }

        [Column("NRO_MATRICULA")] //NUMERIC(38, 0),
        public string NumMatricula { get; set; }

        [Column("NOME_ARQUIVO")] //VARCHAR2(200),
        public string NomeArquivo { get; set; }
        [Column("ARQ_BYTES")] //BLOB,
        public byte[] ArquivoBytes { get; set; }

        [Column("ATIVO")] //NUMERIC(1,0)         default 0,
        public bool Ativo { get; set; }
        [Column("BLOQUEADO")] //NUMERIC(1,0),
        public bool Bloqueado { get; set; }
        [Column("OBSERCACAO")] // VARCHAR2(255),
        public string Observacao { get; set; }
        [Column("NUM_SEQ")] //INTEGER
        public long NumSequencia { get; set; }
    }
}
