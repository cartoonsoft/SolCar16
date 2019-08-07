using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Cart11RI.Entities
{
    [Table("ATO", Schema = "ONZERI")]
    public class ATO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQATO { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPRE { get; set; }

        [StringLength(10)]
        public string LIVRO { get; set; }

        [Column("ATO")]
        [StringLength(100)]
        public string ATO1 { get; set; }
    }
}
