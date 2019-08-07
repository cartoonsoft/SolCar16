namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("DOINOMES", Schema = "ONZERI")]
    public class DOINOMES
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQNOM { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQPRE { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool TIPO { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(200)]
        public string NOME { get; set; }

        public long? DOCUMENTO { get; set; }

        public double? PERC { get; set; }

        [StringLength(1)]
        public string PROCURADOR { get; set; }

        [StringLength(20)]
        public string DOCTOPROC { get; set; }

        [StringLength(20)]
        public string RG { get; set; }

        public long? MATRICULA { get; set; }
    }
}
