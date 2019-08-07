namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("PREMAV", Schema = "ONZERI")]
    public class PREMAV
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPREV { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPRE { get; set; }

        public int? CODEMOLUMENTO { get; set; }

        [StringLength(2)]
        public string TIPOTAB { get; set; }

        public double? VALOR { get; set; }

        public double? OFICIAL { get; set; }

        public double? ESTADO { get; set; }

        public double? IPESP { get; set; }

        public double? RCIVIL { get; set; }

        public double? TJ { get; set; }

        public int? QTDE { get; set; }

        public int? REDUCAO { get; set; }

        public long? SEQISE { get; set; }

        public int? DESCONTO { get; set; }

        [StringLength(1)]
        public string CODAUX { get; set; }

        public double? ISS { get; set; }

        public double? MP { get; set; }
    }
}
