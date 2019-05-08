namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DOICAD", Schema = "DEZESSEIS")]
    public partial class DOICAD
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQ { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DTREGISTRO { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPRE { get; set; }

        public byte? TIPOTRANSACAO { get; set; }

        public int? DTALIENACAO { get; set; }

        public bool? FORMA { get; set; }

        public decimal? VALOR { get; set; }

        [StringLength(20)]
        public string BASE { get; set; }

        [StringLength(100)]
        public string STATUS { get; set; }

        [StringLength(20)]
        public string ESCREVENTE { get; set; }

        public byte? ENVIAR { get; set; }

        public decimal? VALORBASE { get; set; }

        public byte? PERMUTA { get; set; }
    }
}
