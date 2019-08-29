using Dto.CartNew.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI
{
    public class DtoAtoCadastro: DtoEntityBaseModel
    {
        public DtoAtoCadastro()
        {
            this.AtosDocx = new List<DtoAtoDocx>();
        }

        public DtoAtoCadastro(List<DtoAtoDocx> listaAtosDocx)
        {
            this.AtosDocx = listaAtosDocx;
        }

        [Key]
        public long? Id { get; set; }

        public long IdCtaAcessoSist { get; set; }

        public long IdPrenotacao { get; set; }

        public long IdTipoAto { get; set; }

        public string IdUsuarioCadastro { get; set; }

        public string IdUsuarioAlteracao { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public short NumSequenciaAto { get; set; } //seq do ato 

        public DateTime DataAto { get; set; }

        public string NumMatricula { get; set; }

        public bool Ativo { get; set; }

        public bool Bloqueado { get; set; }

        public string Observacao { get; set; }

        List<DtoAtoDocx> AtosDocx { get; set; }

    }
}
