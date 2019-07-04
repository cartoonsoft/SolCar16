namespace Domain.Cartorio.Entities.Cartorio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TABTPS", Schema = "DEZESSEIS")]
    public partial class TABTP
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
        public int DTINI { get; set; }

        public int? DTFIM { get; set; }

        public decimal? VALUOFI { get; set; }

        public decimal? VALUEST { get; set; }

        public decimal? VALUIPE { get; set; }

        public decimal? VALURC { get; set; }

        public decimal? VALUTJ { get; set; }

        [StringLength(20)]
        public string ACRESCIMO { get; set; }

        public decimal? VALPGOFI { get; set; }

        public decimal? VALPGEST { get; set; }

        public decimal? VALPGIPE { get; set; }

        public decimal? VALPGRC { get; set; }

        public decimal? VALPGTJ { get; set; }

        public short? INIPAG { get; set; }

        public decimal? VALUMP { get; set; }

        public decimal? VALUISS { get; set; }

        public decimal? VALPGMP { get; set; }

        public decimal? VALPGISS { get; set; }
    }
}
