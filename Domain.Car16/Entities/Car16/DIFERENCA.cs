namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DIFERENCA", Schema = "DEZESSEIS")]
    public partial class DIFERENCA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DATA { get; set; }

        [StringLength(700)]
        public string DESCRICAO { get; set; }

        public decimal? EMOLUMENTO1 { get; set; }

        public decimal? ESTADO1 { get; set; }

        public decimal? IPESP1 { get; set; }

        public decimal? RCI1 { get; set; }

        public decimal? TJ1 { get; set; }

        [StringLength(1)]
        public string SINAL1 { get; set; }

        public decimal? EMOLUMENTO2 { get; set; }

        public decimal? ESTADO2 { get; set; }

        public decimal? IPESP2 { get; set; }

        public decimal? RCI2 { get; set; }

        public decimal? TJ2 { get; set; }

        [StringLength(1)]
        public string SINAL2 { get; set; }

        public decimal? MP1 { get; set; }

        public decimal? MP2 { get; set; }
    }
}
