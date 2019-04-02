namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.IOBS")]
    public partial class IOB
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQIND { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQOBS { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(200)]
        public string OBS { get; set; }
    }
}
