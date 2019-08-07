namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("LOTENINSCR_PROPRIETARIO", Schema = "ONZERI")]
    public class LOTENINSCR_PROPRIETARIO
    {
        [Key]
        [Column(Order = 0)]
        public decimal ID_LOTENI_P { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal ID_LOTENINSCR { get; set; }

        [StringLength(500)]
        public string PROPRIETARIO { get; set; }
    }
}
