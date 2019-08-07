namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("IREGANTERIOR", Schema = "ONZERI")]
    public class IREGANTERIOR
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQIND { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQREGA { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(200)]
        public string REGANTERIOR { get; set; }
    }
}
