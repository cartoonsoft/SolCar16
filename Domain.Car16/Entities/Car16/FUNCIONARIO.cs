namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.FUNCIONARIOS")]
    public partial class FUNCIONARIO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQFUNC { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte REL { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string NOME { get; set; }
    }
}