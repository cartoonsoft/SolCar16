using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Cartorio.Entities.Diversos
{
    public class DtoDadosImovel
    {
        public DtoDadosImovel()
        {
            //Campos = new List<string>();
            Pessoas = new List<DtoPessoaPesxPre>();
            listaCamposValor = new List<DtoCamposValor>();
        }
        public string NumMatricula { get; set; }

        public string DataAtualExtenso { get; set; }

        public List<DtoPessoaPesxPre> Pessoas { get; set; }

        public long IdMatricula  { get; set; }

        public long IdPrenotacao { get; set; }
        

        //todo: ronaldo arrumar
        //public PREIMO Imovel { get; set; }

        public List<DtoCamposValor> listaCamposValor { get; set; }

    }
}
