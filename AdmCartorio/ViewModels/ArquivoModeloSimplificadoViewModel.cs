using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AdmCartorio.ViewModels
{
    [DataContract]
    public class ArquivoModeloSimplificadoViewModel
    {
        [DataMember]
        public long? Id { get; set; }
        [DataMember]
        public string NomeModelo { get; set; }
        [DataMember]
        public string DescricaoTipoAto { get; set; }
    }
}