using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Cartorio.Entities.Cadastros
{
    [DataContract]
    public class DtoPessoaCartorio  //base antiga
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

        public int? CEP { get; set; }

        [DataMember]
        public string TEL { get; set; }

        public byte? TIPODOC1 { get; set; }

        [DataMember]
        public string NRO1 { get; set; }

        [DataMember]
        public string TIPODOC2 { get; set; }

        [DataMember]
        public string NRO2 { get; set; }
    }
}
