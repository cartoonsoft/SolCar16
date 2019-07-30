namespace Domain.Cart.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FUNCAO", Schema = "DEZESSEIS")]
    public partial class FUNCAO
    {
        [Key]
        [Column(Order = 0)]
        public byte SEQFUN { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string NOM { get; set; }

        [StringLength(50)]
        public string ULTIMO { get; set; }
    }
}
