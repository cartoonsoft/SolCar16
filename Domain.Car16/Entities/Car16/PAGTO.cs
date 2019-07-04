namespace Domain.Cartorio.Entities.Cartorio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PAGTO", Schema = "DEZESSEIS")]
    public partial class PAGTO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQPAG { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DT { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQAUT { get; set; }

        public decimal? VALOR { get; set; }

        [Key]
        [Column(Order = 3)]
        public byte TIPO { get; set; }

        [StringLength(50)]
        public string EMITENTE { get; set; }

        [StringLength(20)]
        public string TELEFONE { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short BANCO { get; set; }

        [StringLength(10)]
        public string AGENCIA { get; set; }

        [StringLength(20)]
        public string CONTA { get; set; }

        [StringLength(10)]
        public string NROCHEQUE { get; set; }

        [StringLength(20)]
        public string CPF { get; set; }

        [StringLength(100)]
        public string NOMINAL { get; set; }

        [StringLength(20)]
        public string TIPOCARTAO { get; set; }

        [StringLength(20)]
        public string NROCARTAO { get; set; }

        [StringLength(10)]
        public string VALIDADE { get; set; }

        [StringLength(1000)]
        public string OBSERVACAO { get; set; }
    }
}
