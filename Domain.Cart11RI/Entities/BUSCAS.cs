namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("BUSCAS", Schema = "ONZERI")]
    public class BUSCAS
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQBUS { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(200)]
        public string PESSOAL { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(200)]
        public string REAL { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(200)]
        public string NUMERO { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(200)]
        public string CONTRIBUINTE { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(200)]
        public string RGCPF { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(200)]
        public string USUCAPIENDO { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(200)]
        public string PROCESSO { get; set; }
    }
}
