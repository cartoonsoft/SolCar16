using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdmCartorio.ViewModels
{
    public class ArquivoModeloDocxListViewModel
    {
        [Key]
        [Display(Name = "Código")]
        public long? Id { get; set; }
        public long IdTipoAto { get; set; }
        public long IdContaAcessoSistema { get; set; }
        public string IdUsuarioCadastro { get; set; }
        public string IdUsuarioAlteracao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        [Display(Name = "Descrição")]
        public string NomeModelo { get; set; }
        [Display(Name = "Nome arquivo")]
        public string CaminhoEArquivo { get; set; }
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }
        [Display(Name = "Descrição tipo")]
        public string DescricaoAto { get; set; }
    }
}