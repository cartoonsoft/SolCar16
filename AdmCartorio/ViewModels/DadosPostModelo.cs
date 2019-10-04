﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdmCartorio.ViewModels
{
    public class DadosPostModelo
    {
        public long Id { get; set; }
        public string NumMatricula { get; set; }
        public long? IdPrenotacao { get; set; }
        public long? IdTipoAto { get; set; }
        public long[] listIdsPessoas { get; set; }
    }
}