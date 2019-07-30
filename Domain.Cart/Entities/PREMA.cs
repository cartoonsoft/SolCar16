namespace Domain.Cart.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PREMA", Schema = "DEZESSEIS")]
    public partial class PREMA
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPRE { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ESP1 { get; set; }

        [StringLength(2)]
        public string ESP2 { get; set; }

        public decimal? DEPOSITO { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long CONFERENTE { get; set; }

        [StringLength(50)]
        public string FUNCIONARIO { get; set; }

        [StringLength(40)]
        public string EMAIL { get; set; }

        [StringLength(200)]
        public string CHEQUE { get; set; }

        [StringLength(50)]
        public string TELEFONE { get; set; }

        [StringLength(1000)]
        public string OBS { get; set; }

        [StringLength(1)]
        public string CANCELADO { get; set; }
    }
}
