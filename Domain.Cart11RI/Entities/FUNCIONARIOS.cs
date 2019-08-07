namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("FUNCIONARIOS", Schema = "ONZERI")]
    public class FUNCIONARIOS
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQFUNC { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int REL { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string NOME { get; set; }
    }
}
