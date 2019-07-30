﻿using Domain.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Cartorio.Entities.Diversos
{
    public class DtoArquivoModeloDocxList
    {
        [Key]
        public long? Id { get; set; }

        public long IdTipoAto { get; set; }

        public long IdContaAcessoSistema { get; set; }

        public string IdUsuarioCadastro { get; set; }

        public string IdUsuarioAlteracao { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string NomeModelo { get; set; }

        public string CaminhoEArquivo { get; set; }

        public bool Ativo { get; set; }

        public string DescricaoAto { get; set; }
    }
}
