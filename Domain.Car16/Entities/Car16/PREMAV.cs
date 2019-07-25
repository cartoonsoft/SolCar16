namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PREMAV", Schema = "DEZESSEIS")]
    public partial class PREMAV
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPREV { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPRE { get; set; }

        public byte? CODEMOLUMENTO { get; set; }

        [StringLength(2)]
        public string TIPOTAB { get; set; }

        public decimal? VALOR { get; set; }

        public decimal? OFICIAL { get; set; }

        public decimal? ESTADO { get; set; }

        public decimal? IPESP { get; set; }

        public decimal? RCIVIL { get; set; }

        public decimal? TJ { get; set; }

        public byte? QTDE { get; set; }

        public byte? REDUCAO { get; set; }

        public int? SEQISE { get; set; }

        public byte? DESCONTO { get; set; }

        [StringLength(1)]
        public string CODAUX { get; set; }

        public decimal? ISS { get; set; }

        public decimal? MP { get; set; }
    }
}
