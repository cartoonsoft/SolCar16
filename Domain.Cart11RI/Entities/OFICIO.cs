namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("OFICIO", Schema = "ONZERI")]
    public class OFICIO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQOFI { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string APRESENTANTE { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string APRESENTANTE1 { get; set; }

        [StringLength(100)]
        public string TELEFONE { get; set; }

        [StringLength(100)]
        public string CONTATO { get; set; }

        public long? DTENTRADA { get; set; }

        public long? DTRETIRADA { get; set; }

        public long? DTPRONTO { get; set; }

        [StringLength(500)]
        public string OBSERVACAO { get; set; }

        [StringLength(50)]
        public string ESCREVENTE { get; set; }

        public long? DTDEVOLUCAO { get; set; }

        public long? DTREGISTRO { get; set; }

        public long? DTSAIDA { get; set; }
    }
}
