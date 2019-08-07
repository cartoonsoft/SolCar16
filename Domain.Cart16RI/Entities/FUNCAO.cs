namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("FUNCAO", Schema = "DEZESSEIS")]
    public partial class FUNCAO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQFUN { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string NOM { get; set; }

        [StringLength(50)]
        public string ULTIMO { get; set; }
    }
}
