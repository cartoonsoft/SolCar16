﻿using AdmCartorio.Models.Attributes;
using AdmCartorio.Models.Enumeradores;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace AdmCartorio.Models
{
    public class ArquivoModeloDocxViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo nome do modelo é obrigatório", AllowEmptyStrings = false)]
        public string NomeModelo { get; set; }

        [Required(ErrorMessage = "Selecione algum tipo de modelo")]
        public NaturezaArquivoModeloDocx NaturezaArquivoModeloDocx { get; set; }

        [RequiredHttpPostedFileBase(ErrorMessage = "Selecione um arquivo.")]
        [IsWordFile(ErrorMessage = "O arquivo deve ser do tipo '.docx' ")]
        public List<HttpPostedFileBase> Files { get; set; }

        #region | Dados nao obrigatorios |
        public LogArquivoModeloDocxViewModel LogArquivoModeloDocxViewModel { get; set; }

        public string CaminhoArquivo { get; set; }

        public string NomeArquivo { get; set; }

        public string ExtensaoArquivo { get; set; }

        public byte[] ArquivoByte { get; set; }
        #endregion
    }
}