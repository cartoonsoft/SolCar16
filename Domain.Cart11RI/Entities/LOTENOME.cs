namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("LOTENOME", Schema = "ONZERI")]
    public class LOTENOME
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string PASTA { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(120)]
        public string NOME { get; set; }
    }
}
