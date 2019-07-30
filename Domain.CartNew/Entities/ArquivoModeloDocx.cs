using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Base;

namespace Domain.CartNew.Entities
{
    [Table("TB_MODELO_DOC", Schema = "DEZESSEIS_NEW")]
    public class ArquivoModeloDocx : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_MODELO_DOC")]
        public override long? Id { get; set; }

        [Column("ID_TP_ATO")]
        public long IdTipoAto { get; set; }

        [Column("ID_CTA_ACESSO_SIST")]
        public long IdContaAcessoSistema { get; set; }

        [Column("ID_USR_CAD")]
        public string IdUsuarioCadastro { get; set; }

        [Column("ID_USR_ALTER")]
        public string IdUsuarioAlteracao { get; set; }

        [Column("DT_CAD")]
        public DateTime DataCadastro { get; set; }

        [Column("DT_ALTER")]
        public DateTime? DataAlteracao { get; set; }

        [Column("DESCRICAO")]
        public string NomeModelo { get; set; }

        [Column("ARQUIVO")]
        public string CaminhoEArquivo { get; set; }

        //[Column("ARQ_BYTES")]
        //[DataType("BLOB")]
        //public byte[] ArquivoBytes { get; set; }

        [Column("ATIVO")]        
        public bool Ativo { get; set; }

    }
}
