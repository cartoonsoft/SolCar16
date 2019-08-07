namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("DOC_E", Schema = "DEZESSEIS")]
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
