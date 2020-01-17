using Domain.CartNew.Enumerations;
using Dto.CartNew.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI
{
    public class DtoAtoEvento: DtoEntityBaseModel
    {
        public DtoAtoEvento()
        {
            this.TipoEvento = DataBaseOperacoes.undefined;
        }

        public long IdAto { get; set; }

        public DataBaseOperacoes TipoEvento { get; set; }

        public DateTime DataEvento { get; set; }

        public string Descricao { get; set; }

        public string IdUsuario { get; set; }

        public string NomeUsuario { get; set; } 

        public string IP { get; set; }

        public string Observacoes { get; set; }

        public string Status { get; set; }

        public string StatusAnterior { get; set; }
    }
}
