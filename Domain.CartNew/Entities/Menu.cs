using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Domain.Core.Entities.Base;

namespace Domain.CartNew.Entities
{

    [Table("TB_MENU", Schema = "DEZESSEIS_NEW")]
    public class Menu: EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_MENU")]
        public override long? Id { get; set; }

        [Column("ID_MENU_PAI")]
        public long? IdMenuPai { get; set; }

        [Column("ID_CTA_ACESSO_SIST")] 
        public long IdCtaAcessoSist { get; set; }

        [Column("ID_TP_MENU")]
        public long IdTipoMenu { get; set; }

        [Column("ID_ACAO")]
        public long? IdAcao { get; set; }

        [Column("ORDEM")]
        public int? Ordem { get; set; }

        [Column("DESC_MENU")]
        public string DescricaoMenu { get; set; }

        [Column("DESC_MENU_MOB")]
        public string DescricaoMenuMobile { get; set; }

        [Column("ICONE_WEB")]
        public string IconeWeb { get; set; }

        [Column("ICONE_MOB")]
        public string  IconeMobile { get; set; }

        [Column("ATIVO")]
        public bool Ativo { get; set; }

        [Column("EM_MANUT")]
        public bool EmManutencao { get; set; }
    }
}