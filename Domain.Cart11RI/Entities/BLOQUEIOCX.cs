using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Cart11RI.Entities
{

    [Table("BLOQUEIOCX", Schema = "ONZERI")]
    public class BLOQUEIOCX
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DATA { get; set; }
    }
}
