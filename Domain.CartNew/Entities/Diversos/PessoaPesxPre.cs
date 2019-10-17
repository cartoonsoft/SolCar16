using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Enumerations;

namespace Domain.CartNew.Entities.Diversos
{
    public class PessoaPesxPre
    {
        [Key]
        public long IdPessoa { get; set; }
        public long IdPrenotacao { get; set; }
        public string Relacao { get; set; }
        public TipoPessoaPrenotacao TipoPessoa { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public string Telefone { get; set; }
        public string TipoDoc1 { get; set; }
        public string TipoDoc2 { get; set; }
        public string Numero1 { get; set; }
        public string Numero2 { get; set; }
    }
}
