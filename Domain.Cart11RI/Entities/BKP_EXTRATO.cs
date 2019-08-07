namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BKP_EXTRATO", Schema = "ONZERI")]
    public class BKP_EXTRATO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPRE { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DATA { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQFICHA { get; set; }

        public double? EMOLUMENTOS { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(1998)]
        public string ATO { get; set; }

        public int? SEQUSU { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(3)]
        public string SEQIDX { get; set; }
    }
}
