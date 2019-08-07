namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("CIP", Schema = "ONZERI")]
    public class CIP
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQ { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SQL { get; set; }

        public int? NUMCART { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string ADQUIRENTE { get; set; }

        public long? CPF_AD { get; set; }

        public long? CGC_AD { get; set; }

        [StringLength(11)]
        public string RG_AD { get; set; }

        [StringLength(5)]
        public string ORGEMISSOR_AD { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string VENDEDOR { get; set; }

        public long? CPF_VE { get; set; }

        public long? CGC_VE { get; set; }

        public int? CODLOG_VE { get; set; }

        [StringLength(100)]
        public string ENDERECO_VE { get; set; }

        [StringLength(5)]
        public string NUMERO_VE { get; set; }

        [StringLength(50)]
        public string COMPLEMENTO_VE { get; set; }

        [StringLength(50)]
        public string BAIRRO_VE { get; set; }

        [StringLength(50)]
        public string CIDADE_VE { get; set; }

        [StringLength(2)]
        public string ESTADO_VE { get; set; }

        [StringLength(8)]
        public string CEP_VE { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string COMPROMISSARIO { get; set; }

        public long? CPF_CO { get; set; }

        public long? CGC_CO { get; set; }

        [StringLength(11)]
        public string RG_CO { get; set; }

        [StringLength(5)]
        public string ORGEMISSOR_CO { get; set; }

        public int? CODLOG_CO { get; set; }

        [StringLength(100)]
        public string ENDERECO_CO { get; set; }

        [StringLength(5)]
        public string NUMERO_CO { get; set; }

        [StringLength(50)]
        public string COMPLEMENTO_CO { get; set; }

        [StringLength(50)]
        public string BAIRRO_CO { get; set; }

        [StringLength(50)]
        public string CIDADE_CO { get; set; }

        [StringLength(2)]
        public string ESTADO_CO { get; set; }

        [StringLength(8)]
        public string CEP_CO { get; set; }

        public int? REGISTRO { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MATRICULA { get; set; }

        public long? DTAQUISICAO { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DTREGISTRO { get; set; }

        public double? VALOR { get; set; }

        public double? AREA { get; set; }

        public double? TESTADA1 { get; set; }

        public double? TESTADA2 { get; set; }

        public double? TESTADA3 { get; set; }

        public double? TESTADA4 { get; set; }

        public double? TESTADA5 { get; set; }

        public double? AREACONSTRUIDA { get; set; }

        public double? FRACAO { get; set; }
    }
}
