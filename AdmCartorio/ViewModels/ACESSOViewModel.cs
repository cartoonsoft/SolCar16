using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace AdmCartorio.ViewModels
{
    public class ACESSOViewModel
    {
        public ACESSOViewModel()
        {
            this.ListaUsersAcesso = new List<UsuarioAcessoViewModel>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SEQACESSO { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(15)]
        [Display(Description = "Código do programa", Name = "Programa")]
        public string PROGRAMA { get; set; }

        [StringLength(200)]
        [Display(Description = "Descrição do acesso", Name = "Descrição")]
        public string OBS { get; set; }

        public List<UsuarioAcessoViewModel> ListaUsersAcesso { get; set; }
    }
}
