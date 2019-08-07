namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("LOTEINSCR", Schema = "ONZERI")]
    public class LOTEINSCR
    {
        [Key]
        [Column(Order = 0)]
        public decimal ID_LOTEINSCR { get; set; }

        [StringLength(500)]
        public string NR_INSCRICAO { get; set; }

        [StringLength(500)]
        public string NR_REGISTRO { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string DENOMINACAO { get; set; }

        [StringLength(4000)]
        public string OBS { get; set; }
    }
}
