using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AdmCartorio.ViewModels
{
    [DataContract]
    public class ArquivoModeloSimplificadoViewModel
    {
        [DataMember]
        [Required(ErrorMessage ="Campo obrigatório")]
        public long? Id { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string NomeModelo { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string DescricaoTipoAto { get; set; }

    }
}