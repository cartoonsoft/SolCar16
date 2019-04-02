namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.NFE_RPS_OBS")]
    public partial class NFE_RPS_OBS
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQNFE { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string SERIE { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long NUMERO { get; set; }

        [Column(TypeName = "long")]
        public string OBS { get; set; }
    }
}
