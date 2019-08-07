namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("CERTN", Schema = "DEZESSEIS")]
    public partial class CERTN
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PEDIDO { get; set; }

        public long? DTENTRADA { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(70)]
        public string NOME1 { get; set; }

        [StringLength(70)]
        public string NOME2 { get; set; }

        [StringLength(70)]
        public string NOME3 { get; set; }
    }
}
