using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Cart11RI.Entities
{
    [Table("BKP_EXTRATO_DIG", Schema = "ONZERI")]
    public class BKP_EXTRATO_DIG
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPRE { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string SEQIDX { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DATA { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQATO { get; set; }

        public bool? BLOQUEIO { get; set; }

        [StringLength(4000)]
        public string TEXTOASSINA { get; set; }
    }
}
