namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("LOTEAMENTO", Schema = "ONZERI")]
    public class LOTEAMENTO
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string PASTA { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string PLANTA { get; set; }

        [Key]
        [Column("LOTEAMENTO", Order = 2)]
        [StringLength(120)]
        public string LOTEAMENTO1 { get; set; }
    }
}
