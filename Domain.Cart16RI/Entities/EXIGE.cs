namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("EXIGE", Schema = "DEZESSEIS")]
    public partial class EXIGE
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPRE { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DATA { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQFICHA { get; set; }

        public double? EMOLUMENTOS { get; set; }

        [StringLength(1998)]
        public string EXIGENCIA { get; set; }

        public int? SEQUSU { get; set; }
    }
}
