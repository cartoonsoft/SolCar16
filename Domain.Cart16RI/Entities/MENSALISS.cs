namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("MENSALISS", Schema = "DEZESSEIS")]
    public partial class MENSALISS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DATA { get; set; }

        public double? VALORISS { get; set; }

        public double? VALORNEWIPE { get; set; }
    }
}
