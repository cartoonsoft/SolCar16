﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Domain.Car16.Attributes;

namespace AdmCartorio.ViewModels
{
    public class ArquivoModeloDocxViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo nome do modelo é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome do Modelo")]
        public string NomeModelo { get; set; }

        [Required(ErrorMessage = "Selecione algum tipo")]
        [Display(Name = "Tipo de ato")]
        public int IdTipoAto { get; set; }

        [Display(Name = "Descrição")]
        public string DescricaoTipoAto { get; set; }

        //[Required(ErrorMessage = "Selecione algum tipo de modelo")]
        //public NaturezaArquivoModeloDocx NaturezaArquivoModeloDocx { get; set; }

        [RequiredHttpPostedFileBase(ErrorMessage = "Selecione um arquivo.")]
        [IsWordFile(ErrorMessage = "O arquivo deve ser do tipo '.docx' ")]
        [Display(Name = "Modelo")]
        public List<HttpPostedFileBase> Files { get; set; }

        #region | Dados nao obrigatorios |
        public LogArquivoModeloDocxViewModel LogArquivoModeloDocxViewModel { get; set; }

        public string Arquivo { get; set; }

        public byte[] ArquivoByte { get; set; }
        #endregion
    }
}