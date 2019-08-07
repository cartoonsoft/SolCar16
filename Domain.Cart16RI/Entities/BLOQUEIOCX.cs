namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("BLOQUEIOCX", Schema = "DEZESSEIS")]
    public partial class BLOQUEIOCX
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DATA { get; set; }
    }
}
