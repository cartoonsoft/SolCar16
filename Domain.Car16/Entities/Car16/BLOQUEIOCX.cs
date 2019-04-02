namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.BLOQUEIOCX")]
    public partial class BLOQUEIOCX
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DATA { get; set; }
    }
}
