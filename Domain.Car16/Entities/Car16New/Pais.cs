using Domain.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain.Car16.Entities.Car16New
{
    [Table("TB_PAIS")]
    public class Pais : EntityBase
    {
        [Key]
        [Column("ID_PAIS")]
        public override long Id { get; set; }

        [Column("COD_IBGE")]
        public string CodIbge { get; set; }

        [Column("SIGLA_PAIS")]
        public string SiglaPais { get; set; }

        [Column("NOME_PAIS")]
        public string NomePais { get; set; }
    }
}
