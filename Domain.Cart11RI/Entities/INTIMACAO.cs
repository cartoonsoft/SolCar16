namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("INTIMACAO", Schema = "ONZERI")]
    public class INTIMACAO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPRE { get; set; }

        public long? DTENTRADA { get; set; }

        public double? DESPESA { get; set; }

        public double? DEPOSITO { get; set; }
    }
}
