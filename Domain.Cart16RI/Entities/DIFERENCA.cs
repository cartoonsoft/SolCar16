namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("DIFERENCA", Schema = "DEZESSEIS")]
    public partial class DIFERENCA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DATA { get; set; }

        [StringLength(700)]
        public string DESCRICAO { get; set; }

        public double? EMOLUMENTO1 { get; set; }

        public double? ESTADO1 { get; set; }

        public double? IPESP1 { get; set; }

        public double? RCI1 { get; set; }

        public double? TJ1 { get; set; }

        [StringLength(1)]
        public string SINAL1 { get; set; }

        public double? EMOLUMENTO2 { get; set; }

        public double? ESTADO2 { get; set; }

        public double? IPESP2 { get; set; }

        public double? RCI2 { get; set; }

        public double? TJ2 { get; set; }

        [StringLength(1)]
        public string SINAL2 { get; set; }

        public double? MP1 { get; set; }

        public double? MP2 { get; set; }
    }
}
