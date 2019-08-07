namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

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
