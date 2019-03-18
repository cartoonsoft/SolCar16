using Domain.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.Entities.API
{
    [Table("TB_CAMPOS_NAT_DOC")]
    public class CampoArquivoModelo : EntityBase
    {
        [Key]
        [Column("ID_CAMPOS_NAT_DOC")]
        public override long Id{ get; set; }

        [Column("ID_CTA_ACESSO_SIST")]
        public int IdEmpresaLogada
        {
            get { return idEmpresaLogada; }
            set { idEmpresaLogada = 1; }
        }
        private int idEmpresaLogada;

        [Column("ID_NAT_DOC")]
        public int IdNaturezaDocumento { get; set; }

        [Column("NOME_CAMPO"),StringLength(50,ErrorMessage ="Tamanho máximo de 50 caracteres")]
        public string Nome { get; set; }

        [Column("PLACE_HOLDER"),StringLength(50, ErrorMessage = "Tamanho máximo de 50 caracteres")]
        public string PlaceHolder { get; set; }
    }
}
