using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartNew.Entities.Diversos
{
    public class PessoaPesxPre
    {
        public PessoaPesxPre()
        {
            ListaCamposValor = new List<CamposValor>();
        }

        [Key]
        public long IdPessoa { get; set; }
        public string Nome { get; set; }
        public string Relacao { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public int? CEP { get; set; }
        public string Telefone { get; set; }
        public byte? TipoDoc1 { get; set; }
        public string Numero1 { get; set; }
        public string TipoDoc2 { get; set; }
        public string Numero2 { get; set; }
        public string TipoPessoa { get; set; }
        public List<CamposValor> ListaCamposValor { get; set; }
    }
}
