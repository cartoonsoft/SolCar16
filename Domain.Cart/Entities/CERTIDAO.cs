namespace Domain.Cart.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CERTIDAO", Schema = "DEZESSEIS")]
    public partial class CERTIDAO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PEDIDO { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DATA { get; set; }

        [StringLength(90)]
        public string NOME1 { get; set; }

        [StringLength(90)]
        public string NOME2 { get; set; }

        [StringLength(90)]
        public string NOME3 { get; set; }

        [StringLength(90)]
        public string NOME4 { get; set; }

        [StringLength(90)]
        public string APRES { get; set; }

        [StringLength(50)]
        public string FONE { get; set; }

        [StringLength(90)]
        public string ENDER { get; set; }

        [StringLength(20)]
        public string NUMERO { get; set; }

        [StringLength(20)]
        public string APTO { get; set; }

        [StringLength(20)]
        public string LOTE { get; set; }

        [StringLength(20)]
        public string QUADRA { get; set; }

        [StringLength(20)]
        public string AREA { get; set; }

        [StringLength(30)]
        public string SUBD { get; set; }

        [StringLength(30)]
        public string BAIRRO { get; set; }

        [StringLength(90)]
        public string ENDERANTIGO { get; set; }

        [StringLength(20)]
        public string NUMEROANTIGO { get; set; }

        [StringLength(20)]
        public string APTOANTIGO { get; set; }

        [StringLength(20)]
        public string LOTEANTIGO { get; set; }

        [StringLength(20)]
        public string QUADRAANTIGO { get; set; }

        [StringLength(20)]
        public string CONTR { get; set; }

        [StringLength(60)]
        public string MATRICULA { get; set; }

        [StringLength(70)]
        public string OUTROS { get; set; }

        [StringLength(30)]
        public string TRANSCRICAO { get; set; }

        public decimal? DEPOSITO { get; set; }

        public int? DTCERTIDAO { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DTENTREGA { get; set; }

        public int? DTESPERA { get; set; }

        public byte? TIPO { get; set; }

        [StringLength(11)]
        public string NUM { get; set; }

        [StringLength(10)]
        public string PERIODO { get; set; }

        public long? BUSCA { get; set; }

        [StringLength(90)]
        public string EMAIL { get; set; }

        [StringLength(30)]
        public string INSCRICAO { get; set; }

        public decimal? EMOLU { get; set; }

        public decimal? ESTADO { get; set; }

        public decimal? IPESP { get; set; }

        public decimal? RCIVIL { get; set; }

        public decimal? TJUSTICA { get; set; }

        public decimal? TOTAL { get; set; }

        public short? QTDE { get; set; }

        [StringLength(1)]
        public string TIPODESC { get; set; }

        public byte? REDUCAO { get; set; }

        [StringLength(500)]
        public string OBS { get; set; }

        public int? CONTROLE { get; set; }

        public int? DTCANCELA { get; set; }

        public int? DTPROMETIDA { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(15)]
        public string ARISP { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PORTAL { get; set; }

        [StringLength(20)]
        public string FUNCIONARIO { get; set; }
    }
}
