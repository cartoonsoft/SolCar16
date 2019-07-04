namespace Domain.Cartorio.Entities.Cartorio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PREIMO", Schema = "DEZESSEIS")]
    public class PREIMO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQIMO { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPRE { get; set; }

        public short? SUBD { get; set; }

        [StringLength(4)]
        public string TIPO { get; set; }

        [StringLength(4)]
        public string TITULO { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string ENDER { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(30)]
        public string NUM { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(15)]
        public string LOTE { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(15)]
        public string QUADRA { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(20)]
        public string APTO { get; set; }

        [StringLength(10)]
        public string BLOCO { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(50)]
        public string EDIF { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(20)]
        public string VAGA { get; set; }

        [StringLength(300)]
        public string OUTROS { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MATRI { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TRANS { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int INSCR { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HIPO { get; set; }

        [Key]
        [Column(Order = 13)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RD { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(30)]
        public string CONTRIB { get; set; }
    }
}
