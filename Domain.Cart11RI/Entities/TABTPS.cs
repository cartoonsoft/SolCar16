namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("TABTPS", Schema = "ONZERI")]
    public class TABTPS
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQTABTPS { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQTP2 { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DTINI { get; set; }

        public long? DTFIM { get; set; }

        public double? VALUOFI { get; set; }

        public double? VALUEST { get; set; }

        public double? VALUIPE { get; set; }

        public double? VALURC { get; set; }

        public double? VALUTJ { get; set; }

        [StringLength(20)]
        public string ACRESCIMO { get; set; }

        public double? VALPGOFI { get; set; }

        public double? VALPGEST { get; set; }

        public double? VALPGIPE { get; set; }

        public double? VALPGRC { get; set; }

        public double? VALPGTJ { get; set; }

        public int? INIPAG { get; set; }

        public double? VALUMP { get; set; }

        public double? VALUISS { get; set; }

        public double? VALPGMP { get; set; }

        public double? VALPGISS { get; set; }
    }
}
