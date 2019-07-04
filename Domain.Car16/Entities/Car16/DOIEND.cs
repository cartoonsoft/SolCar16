namespace Domain.Cartorio.Entities.Cartorio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DOIEND", Schema = "DEZESSEIS")]
    public partial class DOIEND
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQIMO { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPRE { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MATRICULA { get; set; }

        public byte? TIPOIMOVEL { get; set; }

        public byte? ANDAMENTO { get; set; }

        public byte? LOCALIZACAO { get; set; }

        [StringLength(20)]
        public string AREA { get; set; }

        [StringLength(100)]
        public string ENDERECO { get; set; }

        [StringLength(20)]
        public string NUM { get; set; }

        [StringLength(50)]
        public string COMPLEMENTO { get; set; }

        [StringLength(100)]
        public string BAIRRO { get; set; }

        public int? CEP { get; set; }

        [StringLength(50)]
        public string MUNICIPIO { get; set; }

        [StringLength(2)]
        public string UF { get; set; }

        [StringLength(20)]
        public string CONTRIBUINTE { get; set; }

        public long? REGISTRO { get; set; }

        public decimal? VALOR { get; set; }

        public decimal? VALORBASE { get; set; }

        [StringLength(100)]
        public string OUTROS { get; set; }

        public bool? VALORNAOCONSTA { get; set; }

        public bool? FORMAPAG { get; set; }
    }
}
