namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.USUARIOS")]
    public partial class USUARIO
    {
        [Key]
        [Column(Order = 0)]
        public byte SEQUSU { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string NOM { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string NICK { get; set; }

        [StringLength(10)]
        public string PASSW { get; set; }

        [Key]
        [Column(Order = 3)]
        public byte SEQFUN { get; set; }
    }
}
