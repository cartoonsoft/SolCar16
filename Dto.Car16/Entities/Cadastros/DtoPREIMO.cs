using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Cartorio.Entities.Cadastros
{
    [DataContract]
    public class DtoPREIMO
    {
        [DataMember]
        public long SEQIMO { get; set; }
        [DataMember]
        public long SEQPRE { get; set; }
        [DataMember]
        public short? SUBD { get; set; }
        [DataMember]
        public string TIPO { get; set; }
        [DataMember]
        public string TITULO { get; set; }
        [DataMember]
        public string ENDER { get; set; }
        [DataMember]
        public string NUM { get; set; }
        [DataMember]
        public string LOTE { get; set; }
        [DataMember]
        public string QUADRA { get; set; }
        [DataMember]
        public string APTO { get; set; }
        [DataMember]
        public string BLOCO { get; set; }
        [DataMember]
        public string EDIF { get; set; }
        [DataMember]
        public string VAGA { get; set; }
        [DataMember]
        public string OUTROS { get; set; }
        [DataMember]
        public int MATRI { get; set; }

        public int TRANS { get; set; }

        public int INSCR { get; set; }

        public int HIPO { get; set; }

        public int RD { get; set; }
        [DataMember]
        public string CONTRIB { get; set; }
    }
}
