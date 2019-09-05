using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cartorio11RI.ViewModels
{
    public class ModeloDocxListViewModel
    {
        public ModeloDocxListViewModel(long idCtaAcessoSist)
        {
            this.IdCtaAcessoSist = IdCtaAcessoSist;
        }

        [Key]
        [Display(Name = "Código")]
        public long? Id { get; set; }

        [Display(Name = "Conta acesso")]
        public long IdCtaAcessoSist { get; set; }

        public long IdTipoAto { get; set; }

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

        [Display(Name = "Descrição tipo ato")]
        public string DescricaoTipoAto { get; set; }
    }
}