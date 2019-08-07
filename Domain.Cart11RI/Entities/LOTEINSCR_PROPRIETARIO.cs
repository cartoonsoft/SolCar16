namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("LOTEINSCR_PROPRIETARIO", Schema = "ONZERI")]
    public class LOTEINSCR_PROPRIETARIO
    {
        [Key]
        [Column(Order = 0)]
        public decimal ID_LOTEI_P { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal ID_LOTEINSCR { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(500)]
        public string PROPRIETARIO { get; set; }
    }
}
