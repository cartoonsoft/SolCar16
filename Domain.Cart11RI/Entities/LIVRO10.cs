namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("LIVRO10", Schema = "ONZERI")]
    public class LIVRO10
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQUENCIA { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TRANSCRICAO { get; set; }

        [StringLength(100)]
        public string CAMINHO { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string PAGINA { get; set; }
    }
}
