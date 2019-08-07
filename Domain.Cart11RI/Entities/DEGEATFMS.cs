namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("DEGEATFMS", Schema = "ONZERI")]
    public class DEGEATFMS
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
        public int SEQATFMS { get; set; }

        [StringLength(100)]
        public string BUSCA { get; set; }

        [StringLength(100)]
        public string CARTORIO { get; set; }

        [StringLength(50)]
        public string COMARCA { get; set; }

        [StringLength(10)]
        public string DESIGNA { get; set; }

        [StringLength(10)]
        public string PUBLICADA { get; set; }

        [StringLength(50)]
        public string PERIODO { get; set; }

        [StringLength(100)]
        public string MOTIVO { get; set; }
    }
}
