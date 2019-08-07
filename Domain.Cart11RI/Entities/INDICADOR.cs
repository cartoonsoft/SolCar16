namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("INDICADOR", Schema = "ONZERI")]
    public class INDICADOR
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQIND { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(200)]
        public string NUMERO { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string CODIGO { get; set; }
    }
}
