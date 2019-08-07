namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("PROCURA", Schema = "ONZERI")]
    public class PROCURA
    {
        [Key]
        [StringLength(120)]
        public string NOME { get; set; }

        [StringLength(20)]
        public string RG { get; set; }

        [StringLength(20)]
        public string CPF { get; set; }

        [StringLength(100)]
        public string CARTORIO { get; set; }

        public DateTime? COMUNICADO { get; set; }

        [StringLength(100)]
        public string FONTE { get; set; }

        public DateTime? PUBLICACAO { get; set; }
    }
}
