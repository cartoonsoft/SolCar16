namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.ISUBD")]
    public partial class ISUBD
    {
        [Key]
        [Column(Order = 0)]
        public decimal SEQIND { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQSUBD { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(200)]
        public string SUBD { get; set; }
    }
}
