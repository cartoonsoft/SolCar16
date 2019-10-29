namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.ISENTOS")]
    public partial class ISENTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQISE { get; set; }

        [StringLength(50)]
        public string DESCRICAO { get; set; }

        public int? PEROFI { get; set; }

        public int? PEREST { get; set; }

        public int? PERIPE { get; set; }

        public int? PERRC { get; set; }

        public int? PERTJ { get; set; }

        [StringLength(100)]
        public string OBS { get; set; }

        public int? PERMP { get; set; }

        public int? PERISS { get; set; }
    }
}