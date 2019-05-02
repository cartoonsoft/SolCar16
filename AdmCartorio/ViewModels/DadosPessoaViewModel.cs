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
        public long SEQPES { get; set; }
        [DataMember]
        public string NOM { get; set; }
        [DataMember]
        public string ENDER { get; set; }
        [DataMember]
        public string BAI { get; set; }
        [DataMember]
        public string CID { get; set; }
        [DataMember]
        public string UF { get; set; }
        [DataMember]
        public int? CEP { get; set; }
        [DataMember]
        public string TEL { get; set; }
        [DataMember]
        public byte? TIPODOC1 { get; set; }
        [DataMember]
        public string NRO1 { get; set; }
        [DataMember]
        public string TIPODOC2 { get; set; }
        [DataMember]
        public string NRO2 { get; set; }
        [DataMember]
        public string TipoPessoa { get; set; }
    }
}