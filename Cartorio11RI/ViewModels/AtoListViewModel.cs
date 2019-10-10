using Domain.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartorio11RI.ViewModels
{
    public class AtoListViewModel
    {
        [Key]
        public long? Id { get; set; }

        public long IdTipoAto { get; set; }

        [Display(Name = "Numero da prenotação:")]
        public long IdPrenotacao { get; set; }

        public long IdCtaAcessoSist { get; set; }

        public string DescricaoTipoAto { get; set; }
        public string Codigo { get; set; }

        public string IdUsuarioCadastro { get; set; }

        public string IdUsuarioAlteracao { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataCadastro { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DataAlteracao { get; set; }
        [Display(Name = "Numero da matrícula:")]
        public string NumMatricula { get; set; }

        public string NomeArquivo { get; set; }

        public bool Ativo { get; set; }

        public bool Bloqueado { get; set; }

        public string Observacao { get; set; }

        public long NumSequencia { get; set; }
    }
}
