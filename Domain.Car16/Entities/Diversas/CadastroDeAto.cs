using Domain.Car16.Entities.Car16;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.Entities.Diversas
{
    public class CadastroDeAto
    {
        public PREIMO PREIMO { get; set; }
        public PESSOA PESSOA { get; set; }
        public ArquivoModeloSimplificadoDocxList DtoArquivoModeloSimplificadoDocxList { get; set; }
        public long IdTipoAto { get; set; }
        public string Ato { get; set; }
    }
}
