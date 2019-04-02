namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.TABTPV")]
    public partial class TABTPV
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQTABTPV { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte TIPOTAB { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DTINI { get; set; }

        public int? DTFIM { get; set; }

        public byte? TXEST { get; set; }

        public byte? TXIPE { get; set; }

        public byte? TXRC { get; set; }

        public byte? TXTJ { get; set; }

        public byte? TXMP { get; set; }

        public byte? TXISS { get; set; }
    }
}
