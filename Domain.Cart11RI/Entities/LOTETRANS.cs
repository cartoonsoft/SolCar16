namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("LOTETRANS", Schema = "ONZERI")]
    public class LOTETRANS
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string PASTA { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TRANSCRICAO { get; set; }
    }
}
