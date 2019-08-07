namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("IFICHAI", Schema = "DEZESSEIS")]
    public partial class IFICHAI
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQFIC { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ISEQFICI { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string CODIGO { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string NUMERO { get; set; }
    }
}
