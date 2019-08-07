namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("TOMADOR", Schema = "ONZERI")]
    public class TOMADOR
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQTOMADOR { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(75)]
        public string NOM { get; set; }

        [StringLength(3)]
        public string TIPOENDER { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string ENDER { get; set; }

        [StringLength(10)]
        public string NUMERO { get; set; }

        [StringLength(30)]
        public string COMPLEMENTO { get; set; }

        [StringLength(30)]
        public string BAI { get; set; }

        [StringLength(50)]
        public string CID { get; set; }

        [StringLength(2)]
        public string UF { get; set; }

        public int? CEP { get; set; }

        public int? TIPODOC { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long NRODOC { get; set; }

        [StringLength(75)]
        public string EMAIL { get; set; }

        public long? CCM { get; set; }

        public long? IE { get; set; }
    }
}
