namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("TABTPVX", Schema = "ONZERI")]
    public class TABTPVX
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQTABTPV { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal VALFIM { get; set; }

        public decimal? VALOFI { get; set; }

        public decimal? VALEST { get; set; }

        public decimal? VALIPE { get; set; }

        public decimal? VALRC { get; set; }

        public decimal? VALTJ { get; set; }

        public decimal? VALMP { get; set; }

        public decimal? VALISS { get; set; }
    }
}
