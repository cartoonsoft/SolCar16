namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("CHEQUE_DV", Schema = "ONZERI")]
    public class CHEQUE_DV
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQCHEQUE { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DT_CHEQUE { get; set; }

        public int? BANCO { get; set; }

        [StringLength(20)]
        public string AGENCIA { get; set; }

        [StringLength(20)]
        public string CONTA { get; set; }

        public int? NUMERO { get; set; }

        public int? TITULO { get; set; }

        public double? VALOR { get; set; }

        [StringLength(50)]
        public string NOME { get; set; }
    }
}
