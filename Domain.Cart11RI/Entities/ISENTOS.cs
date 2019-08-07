namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("ISENTOS", Schema = "ONZERI")]
    public class ISENTOS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQISE { get; set; }

        [StringLength(50)]
        public string DESCRICAO { get; set; }

        public long? PEROFI { get; set; }

        public long? PEREST { get; set; }

        public long? PERIPE { get; set; }

        public long? PERRC { get; set; }

        public long? PERTJ { get; set; }

        [StringLength(100)]
        public string OBS { get; set; }

        public long? PERMP { get; set; }

        public long? PERISS { get; set; }
    }
}
