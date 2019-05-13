using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Car16.Entities.Diversos
{
    public class DtoPessoaPesxPre
    {
        [Key]
        public long IdPessoa { get; set; } // "SEQPES" NUMBER(15,0), 

        public string TipoPessoa { get; set; }

        public string Nome { get; set; }  //       	"NOM" CHAR(120 BYTE), 

        public string Endereco { get; set; } // 	"ENDER" CHAR(100 BYTE), 

        public string Bairro { get; set; } //	"BAI" CHAR(50 BYTE), 

        public string Cidade { get; set; } //"CID" CHAR(50 BYTE), 

        public string Uf { get; set; } //"UF" CHAR(2 BYTE), 

        public string CEP { get; set; } //"CEP" NUMBER(9,0), 

        public string Telefone { get; set; } //"TEL" CHAR(100 BYTE), 

        public string TipoDoc1 { get; set; } //"TIPODOC1" NUMBER(3,0), 

        public string NunDoc1 { get; set; } // "NRO1"CHAR(20 BYTE), 

        public string TipoDoc2 { get; set; } // "TIPODOC2" CHAR(20 BYTE), 

        public string NunDoc2 { get; set; } // "NRO2" CHAR(20 BYTE)
    }
}
