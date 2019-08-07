namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("CADESP2", Schema = "DEZESSEIS")]
    public partial class CADESP2
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQESP2 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string DESCRICAO { get; set; }

        public int? DIAPRO { get; set; }

        public int? DIAEXAME { get; set; }
    }
}
