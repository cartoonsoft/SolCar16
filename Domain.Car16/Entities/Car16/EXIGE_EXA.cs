namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EXIGE_EXA", Schema = "DEZESSEIS")]
    public partial class EXIGE_EXA
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQEXA { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DATA { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short SEQFICHA { get; set; }

        public decimal? EMOLUMENTOS { get; set; }

        [StringLength(1998)]
        public string EXIGENCIA { get; set; }

        public byte? SEQUSU { get; set; }
    }
}
