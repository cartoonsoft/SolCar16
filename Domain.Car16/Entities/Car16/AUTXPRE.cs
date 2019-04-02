namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.AUTXPRE")]
    public partial class AUTXPRE
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DT { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQAUT { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPRE { get; set; }

        [StringLength(1)]
        public string TIPODOC { get; set; }

        [Key]
        [Column(Order = 3)]
        public byte TIPOAUT { get; set; }

        public decimal? VALOR { get; set; }
    }
}
