namespace Domain.Cartorio.Entities.Cartorio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("INDICADOR", Schema = "DEZESSEIS")]
    public partial class INDICADOR
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQIND { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string CODIGO { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(200)]
        public string NUMERO { get; set; }
    }
}
