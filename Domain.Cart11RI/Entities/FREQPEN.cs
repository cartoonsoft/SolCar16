namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("FREQPEN", Schema = "ONZERI")]
    public class FREQPEN
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPEN { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CODIGO { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ANO { get; set; }

        public long? DTINICIAL { get; set; }

        public long? DTFINAL { get; set; }

        [StringLength(100)]
        public string QUEM { get; set; }

        [StringLength(100)]
        public string MOTIVO { get; set; }

        [StringLength(400)]
        public string MOTIVO1 { get; set; }
    }
}
