using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartNew.Entities
{
    [Table("TB_USR_ACAO", Schema = "DEZESSEIS_NEW")]
    public class UsuarioAcao 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_USUARIO", Order = 0)]
        public string  IdUsuario { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_ACAO", Order = 1)] 
        public long IdAcao { get; set; }
    }
}
