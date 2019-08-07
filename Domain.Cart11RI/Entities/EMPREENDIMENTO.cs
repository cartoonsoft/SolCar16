namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("EMPREENDIMENTO", Schema = "ONZERI")]
    public class EMPREENDIMENTO
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string NOME { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MATRICULA { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string PROPRIETARIO1 { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string PROPRIETARIO2 { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string PROPRIETARIO3 { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string PROPRIETARIO4 { get; set; }

        public int? PROCESSO { get; set; }

        [StringLength(100)]
        public string DESCRICAO { get; set; }

        public int? CONVENCAO { get; set; }

        public int? SUBDISTRITO { get; set; }

        [StringLength(100)]
        public string ENDERECO { get; set; }

        [StringLength(20)]
        public string NUMERO { get; set; }

        [StringLength(20)]
        public string LOTE { get; set; }

        [StringLength(20)]
        public string QUADRA { get; set; }
    }
}
