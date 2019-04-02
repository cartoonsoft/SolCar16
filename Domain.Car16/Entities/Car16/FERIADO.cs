namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.FERIADOS")]
    public partial class FERIADO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DATAFER { get; set; }

        [StringLength(50)]
        public string DESCFER { get; set; }
    }
}
