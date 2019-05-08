namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SELOREGISTRO", Schema = "DEZESSEIS")]
    public partial class SELOREGISTRO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IDREGISTRO { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(25)]
        public string CODSELO { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(32)]
        public string CODCIA { get; set; }

        [StringLength(14)]
        public string DOC { get; set; }

        [StringLength(30)]
        public string INICIAIS { get; set; }

        [StringLength(15)]
        public string DATAHORA { get; set; }

        [StringLength(20)]
        public string EMOLUMENTOS { get; set; }

        [StringLength(20)]
        public string ESTADO { get; set; }

        [StringLength(20)]
        public string IPESP { get; set; }

        [StringLength(20)]
        public string SANTACASA { get; set; }

        [StringLength(20)]
        public string RCIVIL { get; set; }

        [StringLength(20)]
        public string TJ { get; set; }

        [StringLength(20)]
        public string MP { get; set; }

        [StringLength(20)]
        public string ISS { get; set; }

        [StringLength(20)]
        public string TOTAL { get; set; }

        [StringLength(1000)]
        public string QRCODE { get; set; }

        [StringLength(25)]
        public string SELORETIFICADO { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IDCIA { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IDTABELA { get; set; }
    }
}
