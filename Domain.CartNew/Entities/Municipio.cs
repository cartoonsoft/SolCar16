﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Base;

namespace Domain.CartNew.Entities
{
    [Table("TB_MUNICIPIO", Schema = "DEZESSEIS_NEW")]
    public class Municipio : EntityBase
    {
        [Key]
        [Column("ID_MUNICIPIO")]
        public override long? Id { get; set; }

        [Column("ID_UF")]
        public long IdUf { get; set; }

        [Column("COD_IBGE")]
        public string CodIbge { get; set; }

        [Column("NOME_MUN")]
        public string NomeMunicipio { get; set; }
    }
}
