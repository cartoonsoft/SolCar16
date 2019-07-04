using Domain.Cartorio.Entities.Cartorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Cartorio.Entities.Diversas
{
    public class CadastroDeAto
    {
        public PREIMO PREIMO { get; set; }
        public PessoaCartorio Pessoa{ get; set; }
        public ArquivoModeloSimplificadoDocxList DtoArquivoModeloSimplificadoDocxList { get; set; }
        public long IdTipoAto { get; set; }
        public string Ato { get; set; }
    }
}
