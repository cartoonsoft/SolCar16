namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("LOTELA", Schema = "ONZERI")]
    public class LOTELA
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string PASTA { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string LAMARELA { get; set; }
    }
}
