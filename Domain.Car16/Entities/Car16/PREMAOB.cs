namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.PREMAOBS")]
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
