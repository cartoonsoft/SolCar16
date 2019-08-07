namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("SELOCIA", Schema = "ONZERI")]
    public class SELOCIA
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

        public int? CODEMOLUMENTO { get; set; }

        [StringLength(2)]
        public string TIPOTAB { get; set; }

        public double? VALOR { get; set; }

        public double? OFICIAL { get; set; }

        public double? ESTADO { get; set; }

        public double? IPESP { get; set; }

        public double? RCIVIL { get; set; }

        public double? TJ { get; set; }

        public double? MP { get; set; }

        public double? ISS { get; set; }

        public double? TOTAL { get; set; }

        public int? REDUCAO { get; set; }

        public long? SEQISE { get; set; }

        public int? DESCONTO { get; set; }

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
