namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("NFE_RPS_OBS", Schema = "DEZESSEIS")]
    public partial class NFE_RPS_OBS
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQNFE { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string SERIE { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long NUMERO { get; set; }

        [Column(TypeName = "LONG")]
        [StringLength(2147483645)]
        public string OBS { get; set; }
    }
}
