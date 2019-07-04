namespace Domain.Cartorio.Entities.Cartorio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ISUBD", Schema = "DEZESSEIS")]
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
