namespace Domain.Cartorio.Entities.Cartorio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PREMAOBS", Schema = "DEZESSEIS")]
    public partial class PREMAOB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPRE { get; set; }

        public byte? JUSTICAGRATUITA { get; set; }

        public int? QTEC { get; set; }

        [StringLength(1999)]
        public string OBS { get; set; }
    }
}
