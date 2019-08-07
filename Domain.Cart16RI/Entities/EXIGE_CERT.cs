namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("EXIGE_CERT", Schema = "DEZESSEIS")]
    public partial class EXIGE_CERT
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PEDIDO { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DATA { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQFICHA { get; set; }

        public double? EMOLUMENTOS { get; set; }

        [StringLength(1998)]
        public string EXIGENCIA { get; set; }

        public int? SEQUSU { get; set; }
    }
}
