using Dto.CartNew.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI
{
    public class DtoAto: DtoEntityBaseModel
    {
        [Key]
        public override long? Id { get; set; }
                
        public long IdCtaAcessoSist { get; set; } //ID_CTA_ACESSO_SIST NUMERIC(38, 0)       not null,

        public long IdLivro { get; set; }

        public long IdTipoAto { get; set; }

        public long IdPrenotacao { get; set; }

        public long IdModeloDoc { get; set; }

        public string IdUsuarioCadastro { get; set; }

        public string IdUsuarioAlteracao { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public short NumSequenciaAto { get; set; } //numeric(5,0)

        public DateTime? DataAto { get; set; }

        public string NumMatricula { get; set; } //NRO_MATRICULA        VARCHAR2(20)

        public string DescricaoAto { get; set; }

        public string Texto { get; set; }

        public string StatusAto { get; set; }

        public bool Ativo { get; set; }

        public string Observacao { get; set; } // VARCHAR2(512)

        public short NumFicha { get; set; }  //numero da fihca informado pelo usuario

        public short TextoDistanciaTopo { get; set; } //distancia do inicio di texto do topa da pagina (cm)

        public bool DocxGerado { get; set; }

    }
}
