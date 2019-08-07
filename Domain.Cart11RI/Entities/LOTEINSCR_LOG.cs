namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("LOTEINSCR_LOG", Schema = "ONZERI")]
    public class LOTEINSCR_LOG
    {
        [Key]
        [Column(Order = 0)]
        public decimal ID_LOTEINSCR { get; set; }

        public decimal? SEQOPE { get; set; }

        public int? SEQUSU { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string NICK { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime DT { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string OBS { get; set; }
    }
}
