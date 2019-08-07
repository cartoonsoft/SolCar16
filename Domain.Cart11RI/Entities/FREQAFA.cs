namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("FREQAFA", Schema = "ONZERI")]
    public class FREQAFA
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQAFA { get; set; }

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

        [StringLength(50)]
        public string TIPO { get; set; }

        [StringLength(100)]
        public string MOTIVO { get; set; }

        [StringLength(50)]
        public string CONCEDIDO { get; set; }

        public long? DTPUBLICACAO { get; set; }
    }
}
