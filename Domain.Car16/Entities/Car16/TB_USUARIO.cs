namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.TB_USUARIO")]
    public partial class TB_USUARIO
    {
        [Key]
        public decimal ID_USUARIO { get; set; }

        public decimal ID_FUNCAO { get; set; }

        [Required]
        [StringLength(50)]
        public string NOM_USUARIO { get; set; }

        [Required]
        [StringLength(50)]
        public string NOM_NICK { get; set; }

        [StringLength(50)]
        public string NOM_PASSWORD { get; set; }

        public virtual TB_FUNCAO TB_FUNCAO { get; set; }
    }
}
