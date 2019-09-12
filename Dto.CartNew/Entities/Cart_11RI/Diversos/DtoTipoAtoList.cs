using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI.Diversos
{
    public class DtoTipoAtoList
    {
        public DtoTipoAtoList()
        {
            ListaTipoAtosFihos = new List<DtoTipoAtoList>();
        }

        [Key]
        public long? Id { get; set; }

        public long IdCtaAcessoSist { get; set; }

        public long? IdTipoAtoPai { get; set; }

        public string Descricao { get; set; }

        public string Orientacao { get; set; }

        public List<DtoTipoAtoList> ListaTipoAtosFihos { get; set; }
    }
}
