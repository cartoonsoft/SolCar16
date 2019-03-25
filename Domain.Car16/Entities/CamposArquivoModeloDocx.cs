using Domain.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.Entities
{
    [Table("TB_CAMPOS_TB_MOD")]
    public class CamposArquivoModeloDocx : EntityBase
    {
        [Column("ID_CAMPOS_TB_MOD")]
        public override long Id { get; set; }

        [Column("ID_TP_MODELO")]
        public int IdTipoAto { get; set; }

        [Column("ID_CTA_ACESSO_SIST")]
        public int IdAcessoSistema { get; set; }

        [Column("NOME_CAMPO")]
        public string NomeCampo { get; set; }

        [Column("PLACE_HOLDER")]
        public string PlaceHolder { get; set; }

    }
}
