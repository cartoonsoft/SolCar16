using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cartorio11RI.ViewModels
{
    public class InternalServerErrorViewModel
    {
        public long Id { get; set; }

        public string Descricao { get; set; }

        public DateTime Data { get; set; }

        public Exception Excecao { get; set; }

    }
}