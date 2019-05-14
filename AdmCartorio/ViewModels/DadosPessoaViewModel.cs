using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AdmCartorio.ViewModels
{
    [DataContract]
    public class DadosPessoaViewModel
    {
        [DataMember]
        public long IdPessoa { get; set; }
        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public string Endereco { get; set; }
        [DataMember]
        public string Bairro { get; set; }
        [DataMember]
        public string Cidade { get; set; }
        [DataMember]
        public string UF { get; set; }
        [DataMember]
        public int? CEP { get; set; }
        [DataMember]
        public string Telefone { get; set; }
        [DataMember]
        public byte? TipoDoc1 { get; set; }
        [DataMember]
        public string Numero1 { get; set; }
        [DataMember]
        public string TipoDoc2 { get; set; }
        [DataMember]
        public string Numero2 { get; set; }
        [DataMember]
        public string TipoPessoa { get; set; }
    }
}