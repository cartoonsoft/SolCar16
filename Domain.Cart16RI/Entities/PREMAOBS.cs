namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("PREMAOBS", Schema = "DEZESSEIS")]
    public partial class PREMAOBS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPRE { get; set; }

        public int? JUSTICAGRATUITA { get; set; }

        public long? QTEC { get; set; }

        [StringLength(1999)]
        public string OBS { get; set; }
    }
}
