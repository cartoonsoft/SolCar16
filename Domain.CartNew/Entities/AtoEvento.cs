using Domain.CartNew.Enumerations;
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
    [Table("TB_ATO_EVENTO", Schema = "DEZESSEIS_NEW")]
    public class AtoEvento: EntityBase
    {
        public AtoEvento()
        {
            this.TipoEvento = DataBaseOperacoes.undefined;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_ATO_EVENTO")]
        public override long? Id { get; set; }

        [Column("ID_ATO")]
        public long IdAto { get; set; }

        [Column("TP_EVENTO")]
        public DataBaseOperacoes TipoEvento { get; set; }

        [Column("DT_EVENTO")]
        public DateTime DataEvento { get; set; }

        [Column("DESCRICAO")]
        public string Descricao { get; set; }

        [Column("ID_USUARIO")]
        public string IdUsuario{ get; set; }

        [Column("IP")]
        public string IP { get; set; }

        [Column("OBSERVACAO")]
        public string Observacoes { get; set; }

        [Column("STATUS")]
        public string Status { get; set; }

        [Column("STATUS_ANT")]
        public string StatusAnterior { get; set; }
    }
}
