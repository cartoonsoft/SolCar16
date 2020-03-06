using Domain.CartNew.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI
{
    public class DtoAtoPessoa
    {
        [Key]
        public long IdAto { get; set; }

        [Key]
        public long SeqPes { get; set; }

        public TipoPessoaPrenotacao TipoPessoa { get; set; }
    }
}
