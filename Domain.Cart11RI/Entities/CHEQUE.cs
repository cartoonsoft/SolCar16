namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("CHEQUE", Schema = "ONZERI")]
    public class CHEQUE
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQCHEQUE { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQ { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string BANCO { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(20)]
        public string AGENCIA { get; set; }

        [StringLength(20)]
        public string CONTA { get; set; }

        [StringLength(20)]
        public string NUMERO { get; set; }

        [StringLength(100)]
        public string TITULO { get; set; }

        public double? VALOR { get; set; }

        [StringLength(300)]
        public string NOME { get; set; }
    }
}
