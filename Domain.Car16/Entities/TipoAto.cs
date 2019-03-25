using Domain.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.Entities
{
    [Table("TB_TP_MODELO")]
    public class TipoAto : EntityBase
    {
        [Column("ID_TP_MODELO")]
        public override long Id { get; set; }

        [Column("ID_CTA_ACESSO_SIST")]
        public int IdAcessoSistema { get; set; }

        [Column("DESCRICAO")]
        public string Descricao { get; set; }

    }
}
