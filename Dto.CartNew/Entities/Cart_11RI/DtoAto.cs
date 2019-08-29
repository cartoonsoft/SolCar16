using Dto.CartNew.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI
{
    public class DtoAto: DtoEntityBaseModel
    {
        public override long? Id { get; set; }

        //ID_CTA_ACESSO_SIST NUMERIC(38, 0)       not null,
        public long IdCtaAcessoSist { get; set; }

        public long IdTipoAto { get; set; }

        public long IdPrenotacao { get; set; }

        public string IdUsuarioCadastro { get; set; }

        public string IdUsuarioAlteracao { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        //numeric(5,0)
        public short NumSequenciaAto { get; set; }

        public string DataAto { get; set; }

        //NRO_MATRICULA        VARCHAR2(20),
        public string NumMatricula { get; set; }

        //NUMERIC(1,0)         default 0,
        public bool Ativo { get; set; }

        //NUMERIC(1,0),
        public bool Bloqueado { get; set; }

        // VARCHAR2(512),
        public string Observacao { get; set; }
    }
}
