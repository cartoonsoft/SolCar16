namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("MATRICULAS", Schema = "DEZESSEIS")]
    public partial class MATRICULAS
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long NUMERO { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQINC { get; set; }

        public long? ENTRADA { get; set; }

        public long? SAIDA { get; set; }

        public int? MOTIVO { get; set; }

        [StringLength(50)]
        public string RESPONSAVEL { get; set; }

        public long? HORAENTRADA { get; set; }

        public long? HORASAIDA { get; set; }
    }
}
