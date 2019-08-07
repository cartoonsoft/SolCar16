namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("CERTIDAOV", Schema = "ONZERI")]
    public class CERTIDAOV
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PEDIDO { get; set; }

        public double? MP { get; set; }

        public double? ISS { get; set; }
    }
}
