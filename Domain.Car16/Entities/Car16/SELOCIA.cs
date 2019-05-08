namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SELOCIA", Schema = "DEZESSEIS")]
    public partial class SELOCIA
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IDCIA { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1)]
        public string TPPROTOCOLO { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(13)]
        public string NRPROTOCOLO { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(3)]
        public string CODATO { get; set; }

        [StringLength(1)]
        public string TPLIVRO { get; set; }

        [StringLength(9)]
        public string NRLIVRO { get; set; }

        [StringLength(1)]
        public string TPATO { get; set; }

        [StringLength(4)]
        public string NRATO { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQATO { get; set; }

        public long? SEQPREV { get; set; }

        public byte? CODEMOLUMENTO { get; set; }

        [StringLength(2)]
        public string TIPOTAB { get; set; }

        public decimal? VALOR { get; set; }

        public decimal? OFICIAL { get; set; }

        public decimal? ESTADO { get; set; }

        public decimal? IPESP { get; set; }

        public decimal? RCIVIL { get; set; }

        public decimal? TJ { get; set; }

        public decimal? MP { get; set; }

        public decimal? ISS { get; set; }

        public decimal? TOTAL { get; set; }

        public byte? REDUCAO { get; set; }

        public int? SEQISE { get; set; }

        public byte? DESCONTO { get; set; }

        [StringLength(1)]
        public string CODAUX { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IDSELO { get; set; }

        public long? IDSELORETIFICADOR { get; set; }

        public long? IDSELORETIFICADO { get; set; }
    }
}
