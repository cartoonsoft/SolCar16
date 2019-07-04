namespace Domain.Cartorio.Entities.Cartorio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CADCLI", Schema = "DEZESSEIS")]
    public partial class CADCLI
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CODIGO { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(120)]
        public string NOME { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(120)]
        public string ABREV { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(120)]
        public string CHAVE { get; set; }
    }
}
