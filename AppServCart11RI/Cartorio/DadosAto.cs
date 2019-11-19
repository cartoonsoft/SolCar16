using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServCart11RI.Cartorio
{
    public class DadosAto
    {
        public long? IdAto { get; set; }

        public long IdTipoAto { get; set; }

        public long IdLivro { get; set; }

        public long IdModeloDoc { get; set; }

        public long IdPrenotacao { get; set; }

        public string NumMatricula { get; set; }

        public DateTime? DataRegPrenotacao { get; set; }

        public DateTime? DataAto { get; set; }
    }
}
