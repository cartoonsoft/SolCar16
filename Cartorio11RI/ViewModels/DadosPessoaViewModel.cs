using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Cartorio11RI.ViewModels
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
        [Display(Description ="Tipo Documento",Name = "Tipo Documento")]
        public string TipoDoc1 { get; set; }
        [DataMember]
        [Display(Description ="N.° Documento",Name = "N.° Documento")]
        public string Numero1 { get; set; }
        [DataMember]
        [Display(Description = "Tipo Doc2", Name = "Tipo Doc2")]
        public string TipoDoc2 { get; set; }
        [DataMember]
        public string Numero2 { get; set; }
        [DataMember]
        [Display(Description ="TP Pessoa",Name = "TP Pessoa")]
        public string TipoPessoa { get; set; }
    }
}