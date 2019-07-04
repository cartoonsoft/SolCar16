namespace Domain.Cartorio.Entities.Cartorio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MENSALISS", Schema = "DEZESSEIS")]
    public partial class MENSALISS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DATA { get; set; }

        public decimal? VALORISS { get; set; }

        public decimal? VALORNEWIPE { get; set; }
    }
}
