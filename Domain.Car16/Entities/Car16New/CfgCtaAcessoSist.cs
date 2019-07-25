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
    [Table("TB_CFG_CTA_A_SIST", Schema = "DEZESSEIS_NEW")]
    public class CfgCtaAcessoSist : EntityBase //config conta acesso sistema
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_CFG_CTA_A_SIST")]
        public override long? Id { get; set; }

        [Column("ID_CTA_ACESSO_SIST")]
        public long IdContaAcessoSistema { get; set; }

        [Column("ID_TP_CFG")]
        public long IdTipoConfig { get; set; }

        [Column("ID_NUVEM")]
        public string IdNuvem { get; set; } //id na nuvem

        [Column("DT_ULT_SINC")]
        public DateTime DataUltSinc { get; set; } //Data d ultima sincronização nuvem

        [Column("FLAG_SINC")]
        public bool FlagSinc { get; set; } //sincronizado com a nuven

        [Column("MODULO")]
        public string Modulo { get; set; }

        [Column("NOME")]
        public string Nome { get; set; }

        [Column("VALOR")]
        public string Valor { get; set; }

        [Column("ATIVO")]
        public bool Ativo { get; set; }
    }
}
