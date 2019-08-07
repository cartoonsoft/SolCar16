namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("POFICIO", Schema = "ONZERI")]
    public class POFICIO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PEDIDO { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ANO { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DTENTRADA { get; set; }

        public long? DTPROMETIDA { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string OFICIO { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DTOFICIO { get; set; }

        public long? DTRECEBE { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string ORGAO { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(100)]
        public string FINALIDADE { get; set; }

        [StringLength(50)]
        public string RESPOSTA { get; set; }

        public long? DTRESPOSTA { get; set; }

        public long? DTSAIDA { get; set; }

        [StringLength(1)]
        public string TIPO { get; set; }

        public double? EMOLUMENTOS { get; set; }

        public long? DTPAGAMENTO { get; set; }

        public int? BUSCADOR { get; set; }

        public int? LAVRADOR { get; set; }
    }
}
