namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("CERTNOFI", Schema = "ONZERI")]
    public class CERTNOFI
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PEDIDO { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQ { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(60)]
        public string NOME1 { get; set; }

        [StringLength(60)]
        public string NOME2 { get; set; }

        [StringLength(60)]
        public string NOME3 { get; set; }

        [StringLength(60)]
        public string NOME4 { get; set; }
    }
}
