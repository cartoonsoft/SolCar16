using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain.CartNew.Enumerations;

namespace Cartorio11RI.ViewModels
{
    public class LogModeloDocxViewModel
    {
        public long Id { get; set; }

        public long IdModeloDocx { get; set; }

        public string IdUsuario { get; set; }

        public string IP { get; set; }

        public TipoLogModeloDocx TipoLogModeloDocx { get; set; }

        public DateTime DataHora { get; set; }

        public string UsuarioSistOp { get; set; }
    }
}