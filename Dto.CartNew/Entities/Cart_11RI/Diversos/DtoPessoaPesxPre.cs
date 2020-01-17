using Domain.CartNew.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI.Diversos
{
    public class DtoPessoaPesxPre: DtoPessoaAto
    {
        public DtoPessoaPesxPre()
        {
            ListaCamposValor = new List<DtoCamposValor>();
        }

        public List<DtoCamposValor> ListaCamposValor { get; set; }
    }
}
