namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("DEGE", Schema = "ONZERI")]
    public class DEGE
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string NOME { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQNOM { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string RG { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(20)]
        public string PROCESSO { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PRONTUARIO { get; set; }

        [StringLength(10)]
        public string CAIXA { get; set; }

        [Column("DEGE")]
        public int? DEGE1 { get; set; }

        [StringLength(80)]
        public string NOMEPAI { get; set; }

        [StringLength(80)]
        public string NOMEMAE { get; set; }

        public long? DTNASCIMENTO { get; set; }

        [StringLength(50)]
        public string LOCAL { get; set; }

        [StringLength(2)]
        public string UF { get; set; }
    }
}
