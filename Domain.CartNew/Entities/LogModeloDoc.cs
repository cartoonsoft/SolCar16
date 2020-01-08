using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Enumerations;
using Domain.Core.Entities.Base;

namespace Domain.CartNew.Entities
{
    [Table("TB_LOG_ARQ_MOD_DOCX", Schema = "DEZESSEIS_NEW")]
    public class LogModeloDoc : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_LOG_ARQ_MOD_DOCX")]
        public override long? Id { get; set; }

        [Column("ID_MODELO_DOC")]
        public long IdModeloDoc { get; set; }

        [Column("ID_USUARIO")]
        public string IdUsuario { get; set; }

        [Column("IP")]
        public string IP { get; set; }

        [Column("TP_LOG_ARQ_MOD_DOCX")]
        public TipoLogModeloDoc TipoLogModeloDoc { get; set; }

        [Column("DTH_LOG")]
        public DateTime DataHora { get; set; }

        [Column("USUARIO_OS")]
        public string UsuarioSistOperacional { get; set; }
    }
}
