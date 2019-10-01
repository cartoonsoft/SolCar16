using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Enumerations;
using Dto.CartNew.Base;

namespace Dto.CartNew.Entities.Cart_11RI
{
    public class DtoLogModeloDoc: DtoEntityBaseModel
    {
        [Key]
        public override long? Id { get; set; }

        public long IdModeloDoc { get; set; }

        public string IdUsuario { get; set; }

        public string IP { get; set; }

        public TipoLogModeloDoc TipoLogModeloDoc { get; set; }

        public DateTime DataHora { get; set; }

        public string UsuarioSistOperacional { get; set; }
    }
}
