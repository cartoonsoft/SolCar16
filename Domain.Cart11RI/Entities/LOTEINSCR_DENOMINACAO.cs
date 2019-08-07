namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("LOTEINSCR_DENOMINACAO", Schema = "ONZERI")]
    public class LOTEINSCR_DENOMINACAO
    {
        [Key]
        [Column(Order = 0)]
        public decimal ID_LOTEI_D { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal ID_LOTEINSCR { get; set; }

        [StringLength(200)]
        public string NR_AVLT { get; set; }

        [StringLength(200)]
        public string NR_AVTR { get; set; }

        public DateTime? DT_AV { get; set; }

        [StringLength(500)]
        public string ANTIGA { get; set; }

        [StringLength(500)]
        public string ATUAL { get; set; }
    }
}
