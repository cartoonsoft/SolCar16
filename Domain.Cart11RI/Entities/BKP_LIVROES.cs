using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Cart11RI.Entities
{
    [Table("BKP_LIVROES", Schema = "ONZERI")]
    public class BKP_LIVROES
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQUENCIA { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TRANSCRICAO { get; set; }

        [StringLength(100)]
        public string CAMINHO { get; set; }
    }
}
