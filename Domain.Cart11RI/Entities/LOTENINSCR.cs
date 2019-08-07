namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("LOTENINSCR", Schema = "ONZERI")]
    public class LOTENINSCR
    {
        [Key]
        [Column(Order = 0)]
        public decimal ID_LOTENINSCR { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string NR_LA { get; set; }

        [StringLength(500)]
        public string NR_PLANTA { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(500)]
        public string DENOMINACAO { get; set; }

        [StringLength(500)]
        public string NR_REGISTRO { get; set; }

        [StringLength(4000)]
        public string OBS { get; set; }
    }
}
