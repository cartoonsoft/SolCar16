namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("FREQUENCIA", Schema = "ONZERI")]
    public class FREQUENCIA
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CODIGO { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string NOME { get; set; }

        [StringLength(50)]
        public string CARGO { get; set; }

        [StringLength(50)]
        public string FUNCAO { get; set; }

        [StringLength(100)]
        public string ENDERECO { get; set; }

        [StringLength(20)]
        public string FONE { get; set; }

        [StringLength(80)]
        public string NOMEPAI { get; set; }

        [StringLength(80)]
        public string NOMEMAE { get; set; }

        [StringLength(10)]
        public string ECIVIL { get; set; }

        public long? DTNASCIMENTO { get; set; }

        [StringLength(50)]
        public string LOCAL { get; set; }

        [StringLength(1)]
        public string NIVELUNIV { get; set; }

        public long? CPF { get; set; }

        public int? IPESP { get; set; }

        public int? LIVRO { get; set; }

        public int? FOLHA { get; set; }

        [StringLength(20)]
        public string RG { get; set; }

        public int? PRONTUARIO { get; set; }

        public int? PRONTUARIOGERAL { get; set; }

        [StringLength(10)]
        public string CAIXA { get; set; }

        [StringLength(50)]
        public string CONJUGE { get; set; }

        public int? DEPENDENTES { get; set; }

        public int? DIR { get; set; }

        [StringLength(15)]
        public string TITELEITOR { get; set; }

        public int? ZONA { get; set; }

        public int? SECAO { get; set; }

        public long? EXPEDICAO { get; set; }

        public long? CRESERVISTA { get; set; }

        [StringLength(1)]
        public string SERIE { get; set; }

        public long? DTEXPEDICAO { get; set; }

        public int? BANCO { get; set; }

        [StringLength(20)]
        public string CC { get; set; }

        public long? EXERCICIO { get; set; }

        public long? RESCISAO { get; set; }

        public int? FILHOS { get; set; }

        public long? ENTRADA { get; set; }

        public long? SAIDA { get; set; }

        [StringLength(15)]
        public string PIS { get; set; }

        [StringLength(15)]
        public string BANCOPIS { get; set; }

        public long? DTCONTRATO { get; set; }

        public long? DTQUINQ { get; set; }

        public long? DTAUXILIAR { get; set; }

        public long? ESCREVENTE3 { get; set; }

        public long? ESCREVENTE2 { get; set; }

        public long? ESCREVENTE1 { get; set; }

        [StringLength(1)]
        public string SINDICATO { get; set; }
    }
}
