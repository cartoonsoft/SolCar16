using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cartorio11RI.ViewModels
{
    public class DadosAtoViewModel
    {
        public long? IdAto { get; set; }
        public long IdTipoAto { get; set; }
        public long IdModeloDoc { get; set; }
        public long IdPrenotacao { get; set; }
        public long IdMatricula { get; set; }
        public long[] ListIdsPessoas { get; set; }
    }
}