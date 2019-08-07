namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("CAIXAC", Schema = "ONZERI")]
    public class CAIXAC
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQUENCIA { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PEDIDO { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DATA { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(4)]
        public string PAGO { get; set; }

        public double? VALOR { get; set; }

        public int? QUANTIDADE { get; set; }

        [StringLength(100)]
        public string OBS { get; set; }
    }
}
