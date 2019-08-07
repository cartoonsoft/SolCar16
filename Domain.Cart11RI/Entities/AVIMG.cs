namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AVIMG", Schema = "ONZERI")]
    public class AVIMG
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQAV { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string LIVRO { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string NUMERO { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(20)]
        public string AV { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(20)]
        public string LOTE { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(20)]
        public string DOCUMENTO { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DATA { get; set; }
    }
}
