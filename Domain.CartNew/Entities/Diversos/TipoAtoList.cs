using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartNew.Entities.Diversos
{
    public class TipoAtoList
    {
        public TipoAtoList()
        {
            ListaTipoAtosFihos = new List<TipoAtoList>();
        }

        [Key]
        public long? Id { get; set; }

        public long IdCtaAcessoSist { get; set; }

        public long? IdTipoAtoPai { get; set; }

        public string Descricao { get; set; }

        public string Orientacao { get; set; }

        public List<TipoAtoList> ListaTipoAtosFihos { get; set; }
    }
}
