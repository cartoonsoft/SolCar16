namespace Domain.Cart.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CERTIDAOV", Schema = "DEZESSEIS")]
    public partial class CERTIDAOV
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PEDIDO { get; set; }

        public decimal? MP { get; set; }

        public decimal? ISS { get; set; }
    }
}
