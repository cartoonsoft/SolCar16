namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.CERTIDAOV")]
    public partial class CERTIDAOV
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PEDIDO { get; set; }

        public decimal? MP { get; set; }

        public decimal? ISS { get; set; }
    }
}
