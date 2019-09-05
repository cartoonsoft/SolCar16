using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Domain.Core.Entities.Base;

namespace Domain.CartNew.Entities
{
    [Table("TB_LIVRO", Schema = "DEZESSEIS_NEW")]
    public class Livro: EntityBase
    {
        [Key]
        [Column("ID_LIVRO")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override long? Id { get; set; }

        [Column("DESCRICAO")]
        public string Descricao { get; set; }

        [Column("OBS")]
        public string Obs { get; set; }
    }
}





