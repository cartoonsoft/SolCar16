using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.CartNew.Enumerations;
using Domain.Core.Entities.Base;

namespace Domain.CartNew.Entities
{
    [Table("TB_ATO", Schema = "DEZESSEIS_NEW")]
    public class Ato: EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID_ATO")]
        public override long? Id { get; set; }

        [Column("ID_CTA_ACESSO_SIST")] 
        public long IdCtaAcessoSist { get; set; }

        [Column("ID_LIVRO")]
        public long IdLivro { get; set; }

        [Column("ID_TP_ATO")]
        public long IdTipoAto { get; set; }

        [Column("ID_PREMA")] //NUMERIC(19, 0),
        public long IdPrenotacao { get; set; }

        [Column("ID_MODELO_DOC")] //NUMERIC(19, 0),
        public long IdModeloDoc { get; set; }

        [Column("ID_USR_CAD")]
        public string IdUsuarioCadastro { get; set; }

        [Column("ID_USR_ALTER")]
        public string IdUsuarioAlteracao { get; set; }

        [Column("DT_CAD")]
        public DateTime DataCadastro { get; set; }

        [Column("DT_ALTER")]
        public DateTime? DataAlteracao { get; set; }

        [Column("SIGLA_SEQ_ATO")]
        public string SiglaSeqAto { get; set; }

        [Column("NUM_SEQ_ATO")] //numeric(5,0)
        public short NumSequenciaAto { get; set; }

        [Column("DT_ATO")] //Date
        public DateTime? DataAto { get; set; }

        [Column("NRO_MATRICULA")]   //NRO_MATRICULA        VARCHAR2(20),
        public string NumMatricula { get; set; }

        [Column("NUM_FICHA")]
        public short NumFicha { get; set; }  //numero da fihca informado pelo usuario

        [Column("DIST_TOPO")]
        public short DistanciaTopo { get; set; } //distancia do inicio di texto do topa da pagina (cm)

        [Column("FOLHA_FICHA")]
        public TipoFolhaFicha FolhaFicha { get; set; }

        [Column("DESCRICAO")]
        public string DescricaoAto { get; set; }

        [Column("TEXTO")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Texto{ get; set; }

        [Column("STATUS_ATO")]
        public string StatusAto { get; set; }

        [Column("ATIVO")] //NUMERIC(1,0)         default 0,
        public bool Ativo { get; set; }

        [Column("OBSERCACAO")] // VARCHAR2(512),
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }
    }
}
