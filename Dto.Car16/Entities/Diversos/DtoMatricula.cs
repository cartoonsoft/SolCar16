using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Cartorio.Entities.Diversos
{
    public class DtoMatricula
    {
        public long MatriculaId { get; set; }
        public string NomeImovel { get; set; }
        public string EnderecoImovel { get; set; }
        public string NomeProprietarioAtual { get; set; }
    }
}
