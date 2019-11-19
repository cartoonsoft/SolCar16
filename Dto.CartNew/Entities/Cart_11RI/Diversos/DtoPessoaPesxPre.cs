using Domain.CartNew.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI.Diversos
{
    public class DtoPessoaPesxPre
    {
        public DtoPessoaPesxPre()
        {
            ListaCamposValor = new List<DtoCamposValor>();
        }

        [Key]
        public long IdPessoa { get; set; }
        public long IdPrenotacao { get; set; }
        public TipoPessoaPrenotacao TipoPessoa { get; set; }
        public string Nome { get; set; }
        public string Relacao { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Telefone { get; set; }
        public string Cep { get; set; }
        public string Uf { get; set; }
        public string TipoDoc1 { get; set; }
        public string Numero1 { get; set; }
        public string TipoDoc2 { get; set; }
        public string Numero2 { get; set; }
        public List<DtoCamposValor> ListaCamposValor { get; set; }
    }
}
