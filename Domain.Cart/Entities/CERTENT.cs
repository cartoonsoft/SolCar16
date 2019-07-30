namespace Domain.Cart.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CERTENT", Schema = "DEZESSEIS")]
    public partial class CERTENT
    {
        [Key]
        [Column(Order = 0)]
        public byte SEQESP { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string DESCRICAO { get; set; }

        public byte? DIAPRO { get; set; }

        public long? SEQFUNC { get; set; }
    }
}
