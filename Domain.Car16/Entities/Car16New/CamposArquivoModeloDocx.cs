using Domain.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.Entities.Car16New
{
    [Table("TB_CAMPOS_TB_MOD", Schema = "DEZESSEIS_NEW")]
    public class CamposArquivoModeloDocx : EntityBase
    {
        [Column("ID_CAMPOS_TB_MOD")]
        public override long? Id { get; set; }

        [Column("ID_TP_ATO")]
        public long IdTipoAto { get; set; }

        [Column("ID_CTA_ACESSO_SIST")]
        public long IdAcessoSistema { get; set; }

        [Column("NOME_CAMPO")]
        public string NomeCampo { get; set; }

        [Column("PLACE_HOLDER")]
        public string PlaceHolder { get; set; }

        [Column("ENTIDADE")]
        public string Entidade { get; set; }

    }
}
