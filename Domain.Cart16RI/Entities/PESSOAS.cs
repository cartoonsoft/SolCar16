using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Cart16RI.Entities
{
    [Table("PESSOAS", Schema = "DEZESSEIS")]
    public class PESSOAS
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPES { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(120)]
        public string NOM { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string ENDER { get; set; }

        [StringLength(50)]
        public string BAI { get; set; }

        [StringLength(50)]
        public string CID { get; set; }

        [StringLength(2)]
        public string UF { get; set; }

        public int? CEP { get; set; }

        [StringLength(100)]
        public string TEL { get; set; }

        public int? TIPODOC1 { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(20)]
        public string NRO1 { get; set; }

        [StringLength(20)]
        public string TIPODOC2 { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(20)]
        public string NRO2 { get; set; }
    }
}
