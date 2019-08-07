namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("CND", Schema = "DEZESSEIS")]
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

        public long? DTEMISSAO { get; set; }

        public long? DTDIGITACAO { get; set; }

        public long? DTVALIDADE { get; set; }
    }
}
