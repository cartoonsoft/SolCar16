namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.FUNCAO")]
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
