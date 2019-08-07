namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("REGISTRO_1", Schema = "ONZERI")]
    public class REGISTRO_1
    {
        [Key]
        public decimal CD_REGISTRO { get; set; }

        [StringLength(200)]
        public string NR_CONTROLE { get; set; }

        [StringLength(200)]
        public string STEXTO0 { get; set; }

        public DateTime? DT_REGISTRO { get; set; }

        [StringLength(200)]
        public string STEXTO1 { get; set; }

        [StringLength(200)]
        public string STEXTO2 { get; set; }

        [StringLength(200)]
        public string STEXTO3 { get; set; }

        [StringLength(200)]
        public string STEXTO4 { get; set; }

        [StringLength(200)]
        public string STEXTO5 { get; set; }

        [StringLength(200)]
        public string STEXTO6 { get; set; }

        [StringLength(200)]
        public string STEXTO7 { get; set; }

        [StringLength(200)]
        public string STEXTO8 { get; set; }

        [StringLength(200)]
        public string STEXTO9 { get; set; }

        public DateTime? STEXTO10 { get; set; }

        [StringLength(200)]
        public string STEXTO11 { get; set; }

        [StringLength(200)]
        public string STEXTO12 { get; set; }

        [StringLength(200)]
        public string STEXTO13 { get; set; }

        [StringLength(200)]
        public string STEXTO14 { get; set; }

        [StringLength(200)]
        public string STEXTO15 { get; set; }

        [StringLength(200)]
        public string STEXTO16 { get; set; }

        [StringLength(200)]
        public string STEXTO17 { get; set; }

        [StringLength(200)]
        public string STEXTO18 { get; set; }

        [StringLength(200)]
        public string STEXTO19 { get; set; }

        [StringLength(200)]
        public string STEXTO20 { get; set; }

        [StringLength(200)]
        public string STEXTO21 { get; set; }

        [StringLength(200)]
        public string STEXTO22 { get; set; }

        [StringLength(200)]
        public string STEXTO23 { get; set; }

        [StringLength(200)]
        public string STEXTO24 { get; set; }

        [StringLength(200)]
        public string STEXTO25 { get; set; }

        [StringLength(200)]
        public string STEXTO26 { get; set; }

        [StringLength(200)]
        public string STEXTO27 { get; set; }

        [StringLength(200)]
        public string STEXTO28 { get; set; }

        [StringLength(200)]
        public string STEXTO29 { get; set; }

        [StringLength(200)]
        public string STEXTO30 { get; set; }

        [StringLength(200)]
        public string STEXTO31 { get; set; }
    }
}
