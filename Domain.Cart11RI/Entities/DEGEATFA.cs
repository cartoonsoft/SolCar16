namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("DEGEATFA", Schema = "ONZERI")]
    public class DEGEATFA
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string NOME { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQNOM { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQATFA { get; set; }

        [StringLength(100)]
        public string BUSCA { get; set; }

        [StringLength(100)]
        public string CARTORIO { get; set; }

        [StringLength(50)]
        public string COMARCA { get; set; }

        [StringLength(10)]
        public string NOMEACAO { get; set; }

        [StringLength(10)]
        public string HOMOLOGADA { get; set; }

        [StringLength(10)]
        public string PUBLICADA { get; set; }

        [StringLength(10)]
        public string INICIO { get; set; }

        [StringLength(10)]
        public string TERMINO { get; set; }

        [StringLength(10)]
        public string EXONERACAO { get; set; }

        [StringLength(10)]
        public string PUBLICADO { get; set; }
    }
}
