namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("CONTROCXC", Schema = "ONZERI")]
    public class CONTROCXC
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DATA { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool TIPO { get; set; }

        public int? QTE { get; set; }

        public double? DINHEIRO { get; set; }

        public double? MOEDA { get; set; }

        public double? CHEQUE { get; set; }

        public double? CHEQUECARTORIO { get; set; }

        public double? ESTORNO { get; set; }

        public double? CHEQUECARTORIO1 { get; set; }

        public double? SALDO { get; set; }

        public double? CREDITOCONTA { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQUSU { get; set; }

        public double? SITE { get; set; }

        public double? VALEAUT { get; set; }

        public double? VALEARISP { get; set; }
    }
}
