namespace Domain.Cart.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TERMO", Schema = "DEZESSEIS")]
    public partial class TERMO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DATA { get; set; }

        [StringLength(1999)]
        public string DESCRICAO { get; set; }
    }
}
