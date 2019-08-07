using Domain.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartNew.Entities
{
    [Table("TB_CAMPOS_TP_ATO", Schema = "DEZESSEIS_NEW")]
    public class CamposModeloDocx : EntityBase
    {
        [Column("ID_CAMPOS_TP_ATO")]
        public override long? Id { get; set; }

        [Column("ID_TP_ATO")]
        public long IdTipoAto { get; set; }

        [Column("ID_CTA_ACESSO_SIST")]
        public long IdContaAcessoSistema { get; set; }

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
