namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.CND")]
    public partial class CND
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQ { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(120)]
        public string NOME { get; set; }

        [StringLength(10)]
        public string NUMERO { get; set; }

        [StringLength(2)]
        public string SERIE { get; set; }

        [StringLength(3)]
        public string TIPO { get; set; }

        [StringLength(20)]
        public string DOCTO { get; set; }

        [StringLength(20)]
        public string AREA { get; set; }

        [StringLength(20)]
        public string ORGAO { get; set; }

        public int? DTEMISSAO { get; set; }

        public int? DTDIGITACAO { get; set; }

        public int? DTVALIDADE { get; set; }
    }
}
