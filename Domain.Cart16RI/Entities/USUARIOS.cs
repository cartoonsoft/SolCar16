namespace Domain.Cart16RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("USUARIOS", Schema = "DEZESSEIS")]
    public partial class USUARIOS
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQUSU { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string NOM { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string NICK { get; set; }

        [StringLength(10)]
        public string PASSW { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEQFUN { get; set; }
    }
}
