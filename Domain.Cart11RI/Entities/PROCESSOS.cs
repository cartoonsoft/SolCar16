namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("PROCESSOS", Schema = "ONZERI")]
    public class PROCESSOS
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
        public string PROCESSO { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string ACAO { get; set; }

        [StringLength(50)]
        public string ORIGEM { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string REQUERENTE { get; set; }

        public long? DTINFO { get; set; }

        [StringLength(50)]
        public string DESTINO { get; set; }

        public long? DTSAIDA { get; set; }
    }
}
