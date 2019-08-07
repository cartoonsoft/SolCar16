namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BKP_EXTRATO_AD", Schema = "ONZERI")]
    public class BKP_EXTRATO_AD
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

        [StringLength(4000)]
        public string ASSINATURA { get; set; }

        [StringLength(100)]
        public string HASH { get; set; }
    }
}
