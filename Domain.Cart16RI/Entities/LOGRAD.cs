namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("LOGRAD", Schema = "DEZESSEIS")]
    public partial class LOGRAD
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(120)]
        public string NOME { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQ { get; set; }

        [StringLength(600)]
        public string OBS { get; set; }
    }
}
