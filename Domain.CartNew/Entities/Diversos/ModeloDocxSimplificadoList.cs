using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartNew.Entities.Diversos
{
    public class ModeloDocxSimplificadoList
    {
        [Key]
        [Column("ID_MODELO_DOC")]
        public long? Id { get; set; }
        [Column("DESCRICAO")]
        public string NomeModelo { get; set; }
        public string DescricaoTipoAto { get; set; }
    }
}
