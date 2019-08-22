using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cartorio11RI.ViewModels
{
    public class UsuarioAcaoViewModel
    {
        public string IdUsuario { get; set; }

        public long IdCtaAcessoSist { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }

        public string UserName { get; set;}

        public string Email { get; set; }
    }
}
