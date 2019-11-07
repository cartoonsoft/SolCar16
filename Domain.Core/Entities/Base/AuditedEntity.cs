/*-----------------------------------------------------------------------------
  _____            _                    _____        __ _   
/  __ \          | |                  /  ___|      / _| |  
| /  \/ __ _ _ __| |_ ___   ___  _ __ \ `--.  ___ | |_| |_ 
| |    / _` | '__| __/ _ \ / _ \| '_ \ `--. \/ _ \|  _| __|
| \__/\ (_| | |  | || (_) | (_) | | | /\__/ / (_) | | | |_ 
 \____/\__,_|_|   \__\___/ \___/|_| |_\____/ \___/|_|  \__|
Todos os direitos reservados ®                       
------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Core.Entities.Base
{
    /// <summary>
    /// Classse contem os campos de auditoria
    /// </summary>
    public abstract class AuditedEntity : SynchronizableEntity
    {
        [Column("ID_USR_CAD")]
        public string IdUsuarioCadastrou { get; set; }

        [Column("ID_USR_ALTER")]
        public string IdUsuarioAlterou { get; set; }

        [Column("DT_CAD")]
        public DateTime DataCadastro { get; set; }

        [Column("DT_ALTER")]
        public DateTime? DataAlteracao { get; set; }
    }
}
