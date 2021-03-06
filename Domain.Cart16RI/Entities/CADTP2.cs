namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("CADTP2", Schema = "DEZESSEIS")]
    public partial class CADTP2
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQTP2 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string DESCRICAO { get; set; }

        [StringLength(2)]
        public string TAB { get; set; }

        public int? REDUCAO { get; set; }

        [StringLength(1)]
        public string CODAUX { get; set; }
    }
}
