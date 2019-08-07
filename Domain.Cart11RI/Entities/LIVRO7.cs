namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("LIVRO7", Schema = "ONZERI")]
    public class LIVRO7
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQLIV { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string LIVRO { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PAGINA { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string INDICACAO { get; set; }

        [Key]
        [Column(Order = 5)]
        public double POSICAOX { get; set; }

        [Key]
        [Column(Order = 6)]
        public double POSICAOY { get; set; }

        [StringLength(100)]
        public string ARQUIVO { get; set; }

        [StringLength(1000)]
        public string OBS { get; set; }

        [StringLength(2)]
        public string LETRA { get; set; }
    }
}
