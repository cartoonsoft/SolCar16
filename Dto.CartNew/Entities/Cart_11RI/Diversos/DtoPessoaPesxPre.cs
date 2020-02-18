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

        public bool Valido { get; set; } // se pessoa da prenotaçao pode ser inclusa no ato., cpf válido, cadastro ok, etc

        public string RetornoValidacao { get; set; } //descrição do retorno das rotinas de validação da pessoa

        public List<DtoCamposValor> ListaCamposValor { get; set; }
    }
}
