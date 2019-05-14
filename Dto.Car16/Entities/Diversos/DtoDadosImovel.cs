using Domain.Car16.Entities.Car16;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Car16.Entities.Diversos
{
    public class DtoDadosImovel
    {
        public DtoDadosImovel()
        {
            //Campos = new List<string>();
            Pessoas = new List<DtoPessoaPesxPre>();
            CamposValorDadosImovel = new List<DtoCamposValor>();
        }

        public List<DtoPessoaPesxPre> Pessoas { get; set; }

        public long IdMatricula  { get; set; }

        public long IdPrenotacao { get; set; }

        public PREIMO Imovel { get; set; }

        public List<DtoCamposValor> CamposValorDadosImovel { get; set; }

    }
}
