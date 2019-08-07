namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("LOTEINSCR_QUADRA", Schema = "ONZERI")]
    public class LOTEINSCR_QUADRA
    {
        [Key]
        [Column(Order = 0)]
        public decimal ID_LOTEI_Q { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal ID_LOTEINSCR { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string QUADRA { get; set; }

        [StringLength(100)]
        public string LOTE { get; set; }

        [StringLength(500)]
        public string AVERBACAO { get; set; }

        [StringLength(500)]
        public string DEFINITIVA { get; set; }
    }
}
