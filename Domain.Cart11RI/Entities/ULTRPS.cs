namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("ULTRPS", Schema = "ONZERI")]
    public class ULTRPS
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        public string SERIE { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long NUMERO { get; set; }
    }
}
