using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Base;

namespace Domain.Car16.Entities.Car16New
{
    [Table("TB_STATUS", Schema = "DEZESSEIS_NEW")]
    public class Status : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_STATUS")]
        public override long? Id { get; set; }

        [Column("ID_CTA_ACESSO_SIST")]
        public long IdContaAcessoSistema { get; set; }

        [Column("TABELA")]
        public string Tabela { get; set; }

        [Column("CAMPO")]
        public string Campo { get; set; }

        [Column("DESCRICAO")]
        public string Descricao { get; set; }
    }
}
