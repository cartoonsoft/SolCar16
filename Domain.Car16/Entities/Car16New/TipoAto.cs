using Domain.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.Entities.Car16New
{
    [Table("TB_TP_ATO")]
    public class TipoAto : EntityBase
    {
        [Column("ID_TP_ATO")]
        public override long? Id { get; set; }

        [Column("ID_CTA_ACESSO_SIST")]
        public decimal IdAcessoSistema { get; set; }

        [Column("DESCRICAO")]
        public string Descricao { get; set; }

    }
}
