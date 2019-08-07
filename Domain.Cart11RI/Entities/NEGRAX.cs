namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("NEGRAX", Schema = "ONZERI")]
    public class NEGRAX
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQFICHA { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPES { get; set; }

        [StringLength(50)]
        public string OUTROS { get; set; }

        [StringLength(100)]
        public string CONJUGE { get; set; }

        [StringLength(600)]
        public string DESCRICAO1 { get; set; }

        [StringLength(390)]
        public string DESCRICAO2 { get; set; }
    }
}
