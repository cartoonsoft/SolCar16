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
    [Table("TB_NAT_DOC")]
    public class NaturezaCampoArquivoModelo: EntityBase
    {
        [Key]
        [Column("ID_NAT_DOC")]
        public override long Id { get; set; }

        [Column("ID_CTA_ACESSO_SIST")]
        public int IdEmpresaLogada
        {
            get { return idEmpresaLogada; }
            set { idEmpresaLogada = 1; }
        }
        private int idEmpresaLogada;

        [Column("DESCRICAO")]
        public string Descricao { get; set; }

    }
}
