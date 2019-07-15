using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.Entities.Car16New
{
    [Table("TB_USR_ACESSO", Schema = "DEZESSEIS_NEW")]
    public class UsuarioAcesso 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_USUARIO", Order = 0)]
        public string  IdUsuario { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_CTA_ACESSO_SIST", Order = 1)] //ID_CTA_ACESSO_SIST NUMERIC(19, 0)       not null,
        public long IdContaAcessoSistema { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("SEQACESSO", Order = 2)] //	"SEQACESSO" NUMBER(11,0)
        public long SeqAcesso { get; set; }
    }
}
