namespace Domain.Cart.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IBDLIGHT", Schema = "DEZESSEIS")]
    public partial class IBDLIGHT
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQBDL { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQIND { get; set; }

        public long? SEQPRE { get; set; }

        [StringLength(200)]
        public string NOME { get; set; }

        [StringLength(200)]
        public string RG { get; set; }

        [StringLength(200)]
        public string CNPJCPF { get; set; }

        [StringLength(10)]
        public string NMATRICULA { get; set; }

        [StringLength(4000)]
        public string TIPODEATO { get; set; }

        [StringLength(10)]
        public string DTREGAV { get; set; }

        [StringLength(20)]
        public string DTVENDA { get; set; }

        [StringLength(200)]
        public string QUALIFICACAO { get; set; }

        [StringLength(200)]
        public string ATO { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime DTATUALIZACAO { get; set; }
    }
}
