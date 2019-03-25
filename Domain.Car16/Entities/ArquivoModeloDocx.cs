using Domain.Car16.Enumeradores;
using Domain.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.Entities
{
    [Table("TB_MODELO_DOC")]
    public class ArquivoModeloDocx : EntityBase
    {
        [Key]
        [Column("ID_MODELO_DOC")]
        public override long Id { get; set; }

        [Column("ID_TP_MODELO")]
        public int IdTipoAto { get; set; }

        [Column("ID_CTA_ACESSO_SIST")]
        public int IdAcessoSistema { get; set; }

        [Column("ID_USR_CAD")]
        public int IdUsuarioCadastro { get; set; }

        [Column("ID_USR_ALTER")]
        public int? IdUsuarioAlteracao { get; set; }

        [Column("DT_CAD")]
        public DateTime DataCadastro { get; set; }

        [Column("DT_ALTER")]
        public DateTime? DataAlteracao { get; set; }

        [Column("DESCRICAO")]
        public string NomeModelo { get; set; }

        [Column("ARQUIVO")]
        public string CaminhoEArquivo { get; set; }

        [Column("ARQ_BYTES")]
        public byte[] ArquivoBytes { get; set; }
               
        [Column("ATIVO")]
        public bool Ativo { get; set; }

    }
}
