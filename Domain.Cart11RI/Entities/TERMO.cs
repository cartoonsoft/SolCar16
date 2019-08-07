namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("TERMO", Schema = "ONZERI")]
    public class TERMO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DATA { get; set; }

        [StringLength(1000)]
        public string DESCRICAO { get; set; }
    }
}
