namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("TITPOS", Schema = "DEZESSEIS")]
    public partial class TITPOS
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPRE { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQTIT { get; set; }

        [StringLength(20)]
        public string SETOR { get; set; }

        public long? DATAENTRADA { get; set; }

        public long? DATASAIDA { get; set; }

        [StringLength(100)]
        public string CONFERENTE { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DATAMOVTO { get; set; }

        public long? HORAMOVTO { get; set; }

        public long? HORAENTRADA { get; set; }

        public long? HORASAIDA { get; set; }
    }
}
