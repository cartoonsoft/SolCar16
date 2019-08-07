namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("DOC_E_COMP", Schema = "ONZERI")]
    public class DOC_E_COMP
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQ_DOCE { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string NM_TAG { get; set; }

        [StringLength(400)]
        public string NM_CONTEUDO { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQINC { get; set; }
    }
}
