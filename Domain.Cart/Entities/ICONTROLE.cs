namespace Domain.Cart.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ICONTROLE", Schema = "DEZESSEIS")]
    public partial class ICONTROLE
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQCON { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DATA { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HORA { get; set; }

        public long? SEQIND { get; set; }

        [Key]
        [Column(Order = 3)]
        public byte SEQUSU { get; set; }

        [StringLength(80)]
        public string TIPOALT { get; set; }
    }
}
