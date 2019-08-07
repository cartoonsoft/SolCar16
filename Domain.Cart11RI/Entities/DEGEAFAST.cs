namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("DEGEAFAST", Schema = "ONZERI")]
    public class DEGEAFAST
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
        public int SEQAFA { get; set; }

        [StringLength(100)]
        public string BUSCA { get; set; }

        [StringLength(50)]
        public string PERIODO { get; set; }

        [StringLength(10)]
        public string TOTALDIAS { get; set; }

        [StringLength(50)]
        public string NATUREZA { get; set; }

        [StringLength(50)]
        public string AUTORIZADA { get; set; }

        [StringLength(10)]
        public string PUBLICADA { get; set; }
    }
}
