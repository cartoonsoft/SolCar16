using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Cartorio11RI.ViewModels
{
    [DataContract]
    public class ModeloDocxSimplificadoViewModel
    {
        [DataMember]
        public long? Id { get; set; }
        [DataMember]
        public string NomeModelo { get; set; }
        [DataMember]
        public string DescricaoTipoAto { get; set; }

    }
}