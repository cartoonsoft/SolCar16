namespace Domain.Cartorio.Entities.Cartorio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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

        public int? DATAENTRADA { get; set; }

        public int? DATASAIDA { get; set; }

        [StringLength(100)]
        public string CONFERENTE { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DATAMOVTO { get; set; }

        public int? HORAMOVTO { get; set; }

        public int? HORAENTRADA { get; set; }

        public int? HORASAIDA { get; set; }
    }
}
