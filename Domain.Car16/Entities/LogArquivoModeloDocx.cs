using Domain.Car16.Enumeradores;
using Domain.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.Entities
{
    [Table("TB_LOG_ARQUIVO_MODELO_DOCX")]
    public class LogArquivoModeloDocx : EntityBase
    {
        [Key]
        [Column("ID_LOG_ARQUIVO_MODELO_DOCX")]
        public override long Id { get; set; }

        [Column("TIPO_LOG_ARQUIVO_MODELO_DOCX")]
        public TipoLogArquivoModeloDocx TipoLogArquivoModeloDocx { get; set; }

        [Column("ID_ARQUIVO_MODELO_DOCX")]
        public int ArquivoID { get; set; }

        [Column("IP_USUARIO")]
        public string IP { get; set; }

        [Column("NOME_USUARIO")]
        public string NomeUsuario { get; set; }

        [Column("DTH_LOG")]
        public DateTime DataHora { get; set; }
    }
}
