namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.MENSALISS")]
    public partial class MENSALISS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DATA { get; set; }

        public decimal? VALORISS { get; set; }

        public decimal? VALORNEWIPE { get; set; }
    }
}