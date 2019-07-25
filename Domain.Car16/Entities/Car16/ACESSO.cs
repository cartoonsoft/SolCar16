namespace Domain.Car16.Entities.Car16
{
     using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ACESSO",Schema = "DEZESSEIS")]
    public class ACESSO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQACESSO { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(15)]
        public string PROGRAMA { get; set; }

        [StringLength(200)]
        public string OBS { get; set; }
    }
}
