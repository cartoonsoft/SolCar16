using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Base;

namespace Domain.CartNew.Entities
{
    [Table("TB_ACAO", Schema = "DEZESSEIS_NEW")]
    public class Acao : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_ACAO")]
        public override long? Id { get; set; }

        [Column("ID_CTA_ACESSO_SIST")] 
        public long IdContaAcessoSistema { get; set; }

        [Column("SEQACESSO")]
        public long SeqAcesso { get; set; }

        [Column("PROGRAMA")]
        public string Programa { get; set; }

        [Column("OBS")]
        public string Obs { get; set; }

        [Column("DESC_PEQ")]
        public string DescricaoPequeno { get; set; }

        [Column("DESC_MED")]
        public string DescricaoMedio { get; set; }

        [Column("DESC_GRD")]
        public string DescricaoGrande { get; set; }

        [Column("DESC_TIP")]
        public string DescricaoTip { get; set; } //usar tips

        [Column("DESC_BALAO")]
        public string DescricaoBalao { get; set; }

        [Column("ORIENTACAO")]
        public string Orientacao { get; set; } // orientações sobre o que faz a action

        [Column("ACTION")]
        public string Action { get; set; }

        [Column("CONTROLLER")]
        public string Controller { get; set; }

        [Column("PARAMETROS")]
        public string Parametros { get; set; }

        [Column("ICONE_WEB")]
        public string IconeWeb { get; set; }

        [Column("ICONE_MOB")]
        public string IconeMobile { get; set; }

        [Column("ATIVO")]
        public bool Ativo { get; set; }

        [Column("EM_MANUT")]
        public bool EmManutencao { get; set; }
    }
}