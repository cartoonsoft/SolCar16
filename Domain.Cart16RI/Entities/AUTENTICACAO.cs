using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Cart16RI.Entities
{
    [Table("AUTENTICACAO", Schema = "DEZESSEIS")]
    public partial class AUTENTICACAO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DT { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQAUT { get; set; }

        public double? VALOR { get; set; }

        public int? SEQESTORNO { get; set; }

        public int? TIPO { get; set; }

        [StringLength(200)]
        public string OBS { get; set; }

        public int? MODO { get; set; }

        public long? QTEAUT { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQUSU { get; set; }
    }
}
