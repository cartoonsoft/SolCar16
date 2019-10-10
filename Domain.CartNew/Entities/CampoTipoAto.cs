using Domain.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartNew.Entities
{
    [Table("TB_CAMPO_TP_ATO", Schema = "DEZESSEIS_NEW")]
    public class CampoTipoAto : EntityBase
    {
        [Column("ID_CAMPO_TP_ATO")]
        public override long? Id { get; set; }

        [Column("ID_CTA_ACESSO_SIST")]
        public long IdCtaAcessoSist { get; set; }

        [Column("NOME_CAMPO")]
        public string NomeCampo { get; set; }

        [Column("PLACE_HOLDER")]
        public string PlaceHolder { get; set; }

        [Column("ENTIDADE")]
        public string Entidade { get; set; }

        [Column("CAMPO")]
        public string Campo { get; set; }
    }
}
