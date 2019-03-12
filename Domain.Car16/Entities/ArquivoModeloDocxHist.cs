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
    [Table("TB_ARQUIVO_MODELO_DOCX_HIST")]
    public class ArquivoModeloDocxHist : EntityBase
    {
        [Key]
        [Column("ID_ARQUIVO_MODELO_DOCX_HIST")]
        public override long Id { get; set; }
        [Column("ID_ARQUIVO_MODELO_DOCX")]
        public int ArquivoModeloDocx { get; set; }
        [Column("NUM_VERSAO")]
        public int Versao { get; set; }
    }
}
