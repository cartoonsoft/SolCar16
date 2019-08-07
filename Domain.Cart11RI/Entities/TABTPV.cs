namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("TABTPV", Schema = "ONZERI")]
    public class TABTPV
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQTABTPV { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TIPOTAB { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DTINI { get; set; }

        public long? DTFIM { get; set; }

        public int? TXEST { get; set; }

        public int? TXIPE { get; set; }

        public int? TXRC { get; set; }

        public int? TXTJ { get; set; }

        public int? TXMP { get; set; }

        public int? TXISS { get; set; }
    }
}
