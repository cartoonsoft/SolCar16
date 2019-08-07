namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("TITPOS", Schema = "ONZERI")]
    public class TITPOS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPRE { get; set; }

        public long? DATAENTRADA { get; set; }

        public long? HORAENTRADA { get; set; }

        public long? DATASAIDA { get; set; }

        public long? HORASAIDA { get; set; }

        [StringLength(1)]
        public string DEVENTRADA { get; set; }

        [StringLength(1)]
        public string DEVSAIDA { get; set; }

        public long? DATAENTRADAR { get; set; }

        public long? HORAENTRADAR { get; set; }

        public long? DATASAIDAR { get; set; }

        public long? HORASAIDAR { get; set; }

        [StringLength(1)]
        public string DEVENTRADAR { get; set; }

        [StringLength(1)]
        public string DEVSAIDAR { get; set; }

        public long? DATAENTRADAM { get; set; }

        public long? HORAENTRADAM { get; set; }

        public long? DATASAIDAM { get; set; }

        public long? HORASAIDAM { get; set; }

        [StringLength(1)]
        public string DEVENTRADAM { get; set; }

        [StringLength(1)]
        public string DEVSAIDAM { get; set; }

        public long? DATAENTRADAE { get; set; }

        public long? HORAENTRADAE { get; set; }

        public long? DATASAIDAE { get; set; }

        public long? HORASAIDAE { get; set; }

        [StringLength(1)]
        public string DEVENTRADAE { get; set; }

        [StringLength(1)]
        public string DEVSAIDAE { get; set; }

        public long? DATAENTRADAD { get; set; }

        public long? HORAENTRADAD { get; set; }

        public long? DATASAIDAD { get; set; }

        public long? HORASAIDAD { get; set; }

        [StringLength(1)]
        public string DEVENTRADAD { get; set; }

        [StringLength(1)]
        public string DEVSAIDAD { get; set; }

        [StringLength(100)]
        public string CONFERENTE { get; set; }

        [StringLength(100)]
        public string DATA { get; set; }
    }
}
