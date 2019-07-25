namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AUTENTICACAO", Schema = "DEZESSEIS")]
    public partial class AUTENTICACAO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DT { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short SEQAUT { get; set; }

        public decimal? VALOR { get; set; }

        public byte? SEQESTORNO { get; set; }

        public byte? TIPO { get; set; }

        [StringLength(200)]
        public string OBS { get; set; }

        public byte? MODO { get; set; }

        public int? QTEAUT { get; set; }

        [Key]
        [Column(Order = 2)]
        public byte SEQUSU { get; set; }
    }
}
