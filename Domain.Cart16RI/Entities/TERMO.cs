namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("TERMO", Schema = "DEZESSEIS")]
    public partial class TERMO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DATA { get; set; }

        [StringLength(1999)]
        public string DESCRICAO { get; set; }
    }
}
