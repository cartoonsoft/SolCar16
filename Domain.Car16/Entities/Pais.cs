using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities.Base;

namespace Domain.Car16.Entities
{
    [Table("TB_PAIS")]
    [OraSequence("SQ_PAI")]
    public class Pais : EntityCar16
    {
        [Key]
        [Column("ID_PAIS")]
        public override long Id { get; }

        [Column("COD_IBGE")]
        public string CodIbge { get; set; }

        [Column("SIGLA_PAIS")]
        public string SiglaPais { get; set; }

        [Column("NOME_PAIS")]
        public string NomePais { get; set; }
    }
}
