﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain.CartNew.Enumerations;

namespace AdmCartorio.ViewModels
{
    public class LogArquivoModeloDocxViewModel
    {
        public long Id { get; set; }

        public long IdArquivoModeloDocx { get; set; }

        public string IdUsuario { get; set; }

        public string IP { get; set; }

        public TipoLogArquivoModeloDocx TipoLogArquivoModeloDocx { get; set; }

        public DateTime DataHora { get; set; }

        public string UsuarioSistOp { get; set; }
    }
}