using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI.Diversos
{
    public class DtoModeloDocxList
    {
        [Key]
        public long? Id { get; set; }

        public long? IdTipoAto { get; set; }

        public long? IdTipoAtoPai { get; set; }

        public long IdCtaAcessoSist { get; set; }

        public string IdUsuarioCadastro { get; set; }

        public string IdUsuarioAlteracao { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string DescricaoModelo { get; set; }

        public string DescricaoTipo { get; set; }  //TB_TP_ATO

        public string SiglaSeqAto { get; set; } //TB_TP_ATO

        public string Orientacao { get; set; }

        public string CaminhoEArquivo { get; set; }

        public bool Ativo { get; set; }
    }
}
