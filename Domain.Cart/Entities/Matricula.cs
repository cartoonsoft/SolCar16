namespace Domain.Cart.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MATRICULAS", Schema = "DEZESSEIS")]     
    public class Matricula
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long NUMERO { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQINC { get; set; }

        public int? ENTRADA { get; set; }

        public int? SAIDA { get; set; }

        public byte? MOTIVO { get; set; }

        [StringLength(50)]
        public string RESPONSAVEL { get; set; }

        public int? HORAENTRADA { get; set; }

        public int? HORASAIDA { get; set; }
    }
}
