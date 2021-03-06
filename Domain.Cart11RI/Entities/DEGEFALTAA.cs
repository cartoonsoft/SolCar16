namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("DEGEFALTAA", Schema = "ONZERI")]
    public class DEGEFALTAA
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string NOME { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQNOM { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQABO { get; set; }

        [StringLength(100)]
        public string BUSCA { get; set; }

        [StringLength(10)]
        public string ANOS { get; set; }

        [StringLength(100)]
        public string DIAS { get; set; }
    }
}
