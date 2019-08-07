namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("DEGEOPC", Schema = "ONZERI")]
    public class DEGEOPC
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
        public int SEQOPC { get; set; }

        [StringLength(100)]
        public string BUSCA { get; set; }

        [StringLength(50)]
        public string OPCAO { get; set; }

        [StringLength(50)]
        public string PROVIMENTO { get; set; }

        [StringLength(10)]
        public string DATA { get; set; }

        [StringLength(10)]
        public string PARTIR { get; set; }

        [StringLength(10)]
        public string PERMANENTE { get; set; }

        [StringLength(10)]
        public string GERAL { get; set; }
    }
}
