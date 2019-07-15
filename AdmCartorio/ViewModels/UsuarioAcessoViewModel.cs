using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdmCartorio.ViewModels
{
    public class UsuarioAcessoViewModel
    {
        public string IdUsuario { get; set; }

        public long IdContaAcessoSistema { get; set; }

        public long SeqAcesso { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }

        public string UserName { get; set;}

        public string Email { get; set; }
    }
}
