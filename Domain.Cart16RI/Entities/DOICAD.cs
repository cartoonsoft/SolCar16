namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

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
        public long DTREGISTRO { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPRE { get; set; }

        public int? TIPOTRANSACAO { get; set; }

        public long? DTALIENACAO { get; set; }

        public bool? FORMA { get; set; }

        public double? VALOR { get; set; }

        [StringLength(20)]
        public string BASE { get; set; }

        [StringLength(100)]
        public string STATUS { get; set; }

        [StringLength(20)]
        public string ESCREVENTE { get; set; }

        public int? ENVIAR { get; set; }

        public double? VALORBASE { get; set; }

        public int? PERMUTA { get; set; }
    }
}
