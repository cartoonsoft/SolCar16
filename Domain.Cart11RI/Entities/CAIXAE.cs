namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CAIXAE", Schema = "ONZERI")]
    public class CAIXAE
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQUENCIA { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQEXA { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DATA { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(4)]
        public string PAGO { get; set; }

        [Key]
        [Column(Order = 4)]
        public double VALOR { get; set; }

        public int? QUANTIDADE { get; set; }

        [StringLength(100)]
        public string OBS { get; set; }

        [StringLength(10)]
        public string USUARIO { get; set; }
    }
}
