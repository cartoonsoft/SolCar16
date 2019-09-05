﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Domain.CartNew.Attributes;

namespace Cartorio11RI.ViewModels
{
    public class ModeloDocxViewModel
    {
        public ModeloDocxViewModel(long idCtaAcessoSist)
        {
            this.IdCtaAcessoSist = IdCtaAcessoSist;
        }

        [Display(Name = "Código")]
        public long? Id { get; set; }

        [Required(ErrorMessage = "Selecione algum tipo", AllowEmptyStrings = false)]
        [Display(Name = "Tipo de ato")]
        public long IdTipoAto { get; set; }

        [Required(ErrorMessage = "Selecione cont de acesso", AllowEmptyStrings = false)]
        [Display(Name = "Conta acesso")]
        public long IdCtaAcessoSist { get; private set; }

        [Required]
        public string IdUsuarioCadastro { get; }

        public string IdUsuarioAlteracao { get; }

        [Required]
        public DateTime DataCadastro { get; }

        public DateTime? DataAlteracao { get; }

        [Required(ErrorMessage = "O campo nome do modelo é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome do Modelo")]
        public string NomeModelo { get; set; }

        [Display(Name = "Descrição")]
        public string DescricaoTipoAto { get; set; }

        //[Required(ErrorMessage = "Selecione algum tipo de modelo")]
        //public NaturezaArquivoModeloDocx NaturezaArquivoModeloDocx { get; set; }

        [RequiredHttpPostedFileBase(ErrorMessage = "Selecione um arquivo.")]
        [IsWordFile(ErrorMessage = "O arquivo deve ser do tipo '.docx' ")]
        [Display(Name = "Modelo")]
        public List<HttpPostedFileBase> Files { get; set; }

        public LogModeloDocxViewModel logModeloDocxViewModel { get; set; }

        public string CaminhoEArquivo { get; set; }

        [Required]
        public bool Ativo { get; set; }

        public string IpLocal { get; set; }

    }
}