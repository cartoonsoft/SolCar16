namespace Domain.Cart.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ULTRPS", Schema = "DEZESSEIS")]
    public partial class ULTRP
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        public string SERIE { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long NUMERO { get; set; }
    }
}
