using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Enumerations;

namespace Dto.Car16.Entities.Cadastros
{
    public class DtoLogArquivoModeloDocx 
    {
        [Key]
        public long? Id { get; set; }

        public long IdArquivoModeloDocx { get; set; }

        public string IdUsuario { get; set; }

        public string IP { get; set; }

        public TipoLogArquivoModeloDocx TipoLogArquivoModeloDocx { get; set; }

        public DateTime DataHora { get; set; }

        public string UsuarioSistOperacional { get; set; }
    }
}
