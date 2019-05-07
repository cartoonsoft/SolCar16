using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdmCartorio.ViewModels
{
    public class DadosPostArquivoUsuario
    {
        public long Id { get; set; }
        public string Ip { get; set; }
        public string Arquivo { get; set; }
    }
}