using Domain.CartNew.Enumerations;
using Domain.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Base
{
    public class DtoExecProc
    {
        public DtoExecProc()
        {
            this.Id = Guid.NewGuid();
            this.Entidade = null;

            this.Operacao = DataBaseOperacoes.undefined;
            this.TipoMsg = TipoMsgResposta.undefined;

            this.Resposta = false;
            this.Msg = "";
        }

        public Guid Id { get; private set; } //id do processo
        public EntityBase Entidade { get; set; } 

        public DataBaseOperacoes Operacao { get; set; }
        public TipoMsgResposta TipoMsg { get; set; }
        public bool Resposta { get; set; }
        public string Msg { get; set; }   
    }
}
