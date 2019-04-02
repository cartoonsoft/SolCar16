namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.CADESP2")]
    public partial class CADESP2
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQESP2 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string DESCRICAO { get; set; }

        public byte? DIAPRO { get; set; }

        public byte? DIAEXAME { get; set; }
    }
}
