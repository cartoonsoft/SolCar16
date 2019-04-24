using Domain.Car16.enums;
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
    [Table("TB_LOG_ARQ_MOD_DOCX")]
    public class LogArquivoModeloDocx : EntityBase
    {
        [Key]
        [Column("ID_LOG_ARQ_MOD_DOCX")]
        public override long? Id { get; set; }

        [Column("TP_LOG_ARQ_MOD_DOCX")]
        public TipoLogArquivoModeloDocx TipoLogArquivoModeloDocx { get; set; }

        [Column("ID_MODELO_DOC")]
        public long IdArquivoModeloDocx { get; set; }

        [Column("IP_USUARIO")]
        public string IP { get; set; }

        [Column("ID_USUARIO")]
        public string IdUsuario { get; set; }

        [Column("DTH_LOG")]
        public DateTime DataHora { get; set; }
    }
}
