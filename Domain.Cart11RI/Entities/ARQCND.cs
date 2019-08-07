using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Cart11RI.Entities
{

    [Table("ARQCND", Schema = "ONZERI")]
    public class ARQCND
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQ { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string NOME { get; set; }

        [StringLength(10)]
        public string NUMERO { get; set; }

        [StringLength(2)]
        public string SERIE { get; set; }

        [StringLength(3)]
        public string TIPO { get; set; }

        [StringLength(17)]
        public string DOCUMENTO { get; set; }

        [StringLength(10)]
        public string AREA { get; set; }

        [StringLength(16)]
        public string ORGAO { get; set; }

        public long? DTEMISSAO { get; set; }

        public long? DTVALIDADE { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int POSTO { get; set; }
    }
}
