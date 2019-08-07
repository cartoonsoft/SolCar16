namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("PREMAOBS", Schema = "ONZERI")]
    public class PREMAOBS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPRE { get; set; }

        [StringLength(1000)]
        public string OBS { get; set; }
    }
}
