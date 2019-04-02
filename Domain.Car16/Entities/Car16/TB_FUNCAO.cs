namespace Domain.Car16.Entities.Car16
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEZESSEIS.TB_FUNCAO")]
    public partial class TB_FUNCAO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_FUNCAO()
        {
            TB_USUARIO = new HashSet<TB_USUARIO>();
        }

        [Key]
        public decimal ID_FUNCAO { get; set; }

        [Required]
        [StringLength(20)]
        public string NOM_FUNCAO { get; set; }

        [StringLength(50)]
        public string ULTIMO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_USUARIO> TB_USUARIO { get; set; }
    }
}
