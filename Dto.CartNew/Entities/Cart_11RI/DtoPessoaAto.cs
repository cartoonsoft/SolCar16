using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Enumerations;
using Dto.CartNew.Base;

namespace Dto.CartNew.Entities.Cart_11RI
{
    public class DtoPessoaAto: DtoBase  //base antiga
    {
        public long IdPessoa { get; set; }
        public long IdAto { get; set; }
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
