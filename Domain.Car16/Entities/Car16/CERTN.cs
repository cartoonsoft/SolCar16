namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.CERTN")]
    public partial class CERTN
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PEDIDO { get; set; }

        public int? DTENTRADA { get; set; }

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