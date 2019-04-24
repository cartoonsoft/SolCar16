namespace AppBootStrapMin.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS_NEW.TB_MODELO_DOC")]
    public partial class TB_MODELO_DOC
    {
        [Key]
        public decimal ID_MODELO_DOC { get; set; }

        public decimal? ID_TP_ATO { get; set; }

        public decimal ID_CTA_ACESSO_SIST { get; set; }

        public decimal ID_USR_CAD { get; set; }

        public decimal? ID_USR_ALTER { get; set; }

        public DateTime DT_CAD { get; set; }

        public DateTime? DT_ALTER { get; set; }

        [StringLength(200)]
        public string DESCRICAO { get; set; }

        [StringLength(200)]
        public string ARQUIVO { get; set; }

        public byte[] ARQ_BYTES { get; set; }

        public bool? ATIVO { get; set; }
    }
}
