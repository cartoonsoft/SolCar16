namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.IFICHA")]
    public partial class IFICHA
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQFIC { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(200)]
        public string NOME { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(200)]
        public string DOC { get; set; }

        [StringLength(200)]
        public string OBS { get; set; }

        [StringLength(100)]
        public string NOMEARQ { get; set; }
    }
}
