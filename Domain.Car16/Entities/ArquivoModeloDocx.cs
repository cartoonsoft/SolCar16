using Domain.Car16.Enumeradores;
using Domain.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.Entities
{
    [Table("TB_ARQUIVO_MODELO_DOCX")]
    public class ArquivoModeloDocx : EntityBase
    {
        [Key]
        [Column("ID_ARQUIVO_MODELO_DOCX")]
        public override long Id { get; set; }
        [Column("NOME_MODELO")]
        public string NomeModelo { get; set; }
        [Column("TIPO_ARQUIVO_MODELO_DOCX")]
        public NaturezaArquivoModeloDocx NaturezaArquivoModeloDocx { get; set; }
        [Column("NOME_ARQUIVO")]
        public string NomeArquivo { get; set; }
        [Column("CAMINHO_ARQUIVO")]
        public string CaminhoArquivo { get; set; }
        [Column("EXTENSAO_ARQUIVO")]
        public string ExtensaoArquivo { get; set; }
        [Column("BYTES_ARQUIVO")]
        public byte[] ArquivoByte { get; set; }
    }
}
