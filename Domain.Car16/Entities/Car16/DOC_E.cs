namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.DOC_E")]
    public partial class DOC_E
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQ_DOCE { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1)]
        public string TP_SERVICO { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string NR_PROTOCOLO { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPRE { get; set; }
    }
}
