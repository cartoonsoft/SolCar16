namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("EXTRATO", Schema = "ONZERI")]
    public class EXTRATO
    {
        public long? SEQPRE { get; set; }

        public long? DATA { get; set; }

        public int? SEQFICHA { get; set; }

        public double? EMOLUMENTOS { get; set; }

        [StringLength(1998)]
        public string ATO { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQUSU { get; set; }

        [StringLength(100)]
        public string SEQIDX { get; set; }
    }
}
