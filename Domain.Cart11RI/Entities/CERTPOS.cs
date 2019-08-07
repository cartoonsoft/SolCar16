namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("CERTPOS", Schema = "ONZERI")]
    public class CERTPOS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PEDIDO { get; set; }

        public long? DATAENTRADA { get; set; }

        public long? HORAENTRADA { get; set; }

        public long? DATASAIDA { get; set; }

        public long? HORASAIDA { get; set; }

        [StringLength(1)]
        public string DEVENTRADA { get; set; }

        [StringLength(1)]
        public string DEVSAIDA { get; set; }

        public long? DATAENTRADAB { get; set; }

        public long? HORAENTRADAB { get; set; }

        public long? DATASAIDAB { get; set; }

        public long? HORASAIDAB { get; set; }

        [StringLength(1)]
        public string DEVENTRADAB { get; set; }

        [StringLength(1)]
        public string DEVSAIDAB { get; set; }

        public long? DATAENTRADAE { get; set; }

        public long? HORAENTRADAE { get; set; }

        public long? DATASAIDAE { get; set; }

        public long? HORASAIDAE { get; set; }

        [StringLength(1)]
        public string DEVENTRADAE { get; set; }

        [StringLength(1)]
        public string DEVSAIDAE { get; set; }

        [StringLength(10)]
        public string USUARQUIVO { get; set; }

        [StringLength(10)]
        public string USUBUSCA { get; set; }

        [StringLength(10)]
        public string USUEXPEDICAO { get; set; }

        [StringLength(10)]
        public string USUDEVOLUCAO { get; set; }
    }
}
