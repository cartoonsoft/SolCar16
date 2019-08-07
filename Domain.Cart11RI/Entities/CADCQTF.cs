namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("CADCQTF", Schema = "ONZERI")]
    public class CADCQTF
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQ { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string NOME { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(1)]
        public string TIPO { get; set; }

        [StringLength(18)]
        public string DOC { get; set; }

        [StringLength(10)]
        public string NUMERO { get; set; }

        public long? DTEMISSAO { get; set; }

        public long? DTVALIDADE { get; set; }

        [StringLength(500)]
        public string OBS { get; set; }
    }
}
