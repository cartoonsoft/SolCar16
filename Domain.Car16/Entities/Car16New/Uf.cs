using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Base;

namespace Domain.Cartorio.Entities.CartorioNew
{
    [Table("TB_UF", Schema = "DEZESSEIS_NEW")]
    public class Uf : EntityBase
    {
        [Key]
        [Column("ID_UF")]
        public override long? Id { get; set; }

        [Column("ID_PAIS")]
        public long IdPais { get; set; }

        [Column("COD_IBGE")]
        public string CodigoIbge { get; set; }

        [Column("SIGLA")]
        public string Sigla { get; set; }

        [Column("NOME_UF")]
        public string NomeUf { get; set; }
    }
}
