namespace Domain.Cart11RI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("FERIADOS", Schema = "ONZERI")]
    public class FERIADOS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DATAFER { get; set; }

        [StringLength(50)]
        public string DESCFER { get; set; }
    }
}
