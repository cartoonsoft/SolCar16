using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Cart11RI.Entities
{
    [Table("ACESSO", Schema = "ONZERI")]
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
