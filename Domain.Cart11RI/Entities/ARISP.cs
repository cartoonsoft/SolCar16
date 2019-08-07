using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Cart11RI.Entities
{
    [Table("ARISP", Schema = "ONZERI")]
    public class ARISP
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQARI { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(12)]
        public string PEDIDO { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DATA { get; set; }

        [StringLength(90)]
        public string NOME1 { get; set; }

        [StringLength(90)]
        public string NOME2 { get; set; }

        [StringLength(90)]
        public string NOME3 { get; set; }

        [StringLength(90)]
        public string NOME4 { get; set; }

        [StringLength(90)]
        public string ENDER { get; set; }

        [StringLength(15)]
        public string NUMERO { get; set; }

        [StringLength(30)]
        public string LOTE { get; set; }

        [StringLength(30)]
        public string QUADRA { get; set; }

        [StringLength(30)]
        public string BAIRRO { get; set; }

        [StringLength(30)]
        public string APTO { get; set; }

        [StringLength(40)]
        public string EDIFICIO { get; set; }

        [StringLength(30)]
        public string VAGA { get; set; }

        [StringLength(90)]
        public string REQUER { get; set; }

        [StringLength(50)]
        public string FONE { get; set; }

        [StringLength(90)]
        public string EMAIL { get; set; }

        public double? DEPOSITO { get; set; }

        public long? DTCERTIDAO { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DTENTREGA { get; set; }

        public long? DTESPERA { get; set; }

        public bool? TIPO { get; set; }

        public bool? BUSCA { get; set; }

        [StringLength(30)]
        public string MATRICULA { get; set; }

        [StringLength(30)]
        public string TRANSCRICAO { get; set; }

        [StringLength(30)]
        public string INSCRICAO { get; set; }

        public double? EMOLU { get; set; }

        public double? ESTADO { get; set; }

        public double? IPESP { get; set; }

        public double? RCIVIL { get; set; }

        public double? TJUSTICA { get; set; }

        public double? TOTAL { get; set; }

        public int? QTDE { get; set; }

        [StringLength(1)]
        public string TIPODESC { get; set; }

        public int? REDUCAO { get; set; }

        [StringLength(11)]
        public string INTERNET { get; set; }

        [StringLength(500)]
        public string OBS { get; set; }

        public long? CONTROLE { get; set; }

        public long? DTCANCELA { get; set; }
    }
}
