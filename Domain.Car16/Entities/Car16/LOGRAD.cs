namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.LOGRAD")]
    public partial class LOGRAD
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(120)]
        public string NOME { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQ { get; set; }

        [StringLength(600)]
        public string OBS { get; set; }
    }
}
