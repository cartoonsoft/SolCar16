namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("CERTENT", Schema = "DEZESSEIS")]
    public partial class CERTENT
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQESP { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string DESCRICAO { get; set; }

        public int? DIAPRO { get; set; }

        public long? SEQFUNC { get; set; }
    }
}
