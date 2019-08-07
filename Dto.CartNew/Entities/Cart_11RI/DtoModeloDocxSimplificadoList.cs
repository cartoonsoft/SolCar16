using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI
{
    public class DtoModeloDocxSimplificadoList
    {
        [Key]
        [Column("ID_MODELO_DOC")]
        public long? Id { get; set; }
        public string NomeModelo { get; set; }
        public string DescricaoTipoAto { get; set; }
        //public string DescricaoTipoAto { get; set; }
    }
}
