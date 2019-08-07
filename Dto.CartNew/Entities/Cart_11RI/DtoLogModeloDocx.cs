using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Enumerations;

namespace Dto.CartNew.Entities.Cart_11RI
{
    public class DtoLogModeloDocx 
    {
        [Key]
        public long? Id { get; set; }

        public long IdModeloDocx { get; set; }

        public string IdUsuario { get; set; }

        public string IP { get; set; }

        public TipoLogModeloDocx TipoLogModeloDocx { get; set; }

        public DateTime DataHora { get; set; }

        public string UsuarioSistOperacional { get; set; }
    }
}
