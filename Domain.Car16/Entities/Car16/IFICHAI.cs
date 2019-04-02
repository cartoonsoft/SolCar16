namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.IFICHAI")]
    public partial class IFICHAI
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQFIC { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ISEQFICI { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string CODIGO { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string NUMERO { get; set; }
    }
}
