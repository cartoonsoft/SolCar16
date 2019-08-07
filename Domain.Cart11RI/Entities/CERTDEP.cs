namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("CERTDEP", Schema = "ONZERI")]
    public class CERTDEP
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQDEP { get; set; }

        public long? DATADEP { get; set; }

        public double? VALOR { get; set; }

        public long? PEDIDO { get; set; }

        [StringLength(50)]
        public string USUARIO { get; set; }
    }
}
