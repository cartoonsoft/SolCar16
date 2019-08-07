namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("REGISTRO_2", Schema = "ONZERI")]
    public class REGISTRO_2
    {
        [Key]
        [Column(Order = 0)]
        public decimal CD_TABELA { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal CD_REGISTRO { get; set; }

        [StringLength(200)]
        public string STEXTO { get; set; }

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
    }
}
