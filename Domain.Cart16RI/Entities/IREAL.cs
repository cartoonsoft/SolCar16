namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("IREAL", Schema = "DEZESSEIS")]
    public partial class IREAL
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQIND { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQIMO { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(200)]
        public string IMOVEL { get; set; }
    }
}
