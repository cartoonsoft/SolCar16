using Domain.CartNew.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cartorio11RI.ViewModels
{
    public class AtoPessoaViewModel
    {
        public long IdAto { get; set; }

        public long SeqPes { get; set; }

        public TipoPessoaPrenotacao TipoPessoa { get; set; }
    }
}