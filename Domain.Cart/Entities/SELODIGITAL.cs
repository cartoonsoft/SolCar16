namespace Domain.Cart.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SELODIGITAL", Schema = "DEZESSEIS")]
    public partial class SELODIGITAL
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IDSELO { get; set; }

        public int? CNS { get; set; }

        public bool? NATUREZA { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2)]
        public string CODATO { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(13)]
        public string IDATOPRATICADO { get; set; }

        public byte? ANO { get; set; }

        [StringLength(1)]
        public string DV { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IDCIA { get; set; }
    }
}
