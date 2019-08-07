namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("LIVROESOBS", Schema = "ONZERI")]
    public class LIVROESOBS
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TRANSCRICAO { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQOBS { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(200)]
        public string OBS { get; set; }
    }
}
