using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.Entities.Car16New
{
    public class MatriculaAto
    {
        public long MatriculaID { get; set; }
        public string MatriculaEndereco { get; set; }
        public string MatriculaOutorgante { get; set; }
        public string ModeloNome { get; set; }
        public string ModeloTipoAto { get; set; }
        public int NumeroPrenotacao { get; set; }
        public string NomeImovel { get; set; }
        //Pode ter mais de um CPF para o mesmo ato
        public string ModeloCPF { get; set; }
        public string Ato { get; set; }
        public int NumeroAtoSequencia { get; set; }

    }
}
