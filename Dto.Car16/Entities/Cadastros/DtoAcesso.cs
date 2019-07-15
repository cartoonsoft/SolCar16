using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Car16.Entities.Cadastros
{

    public class DtoAcesso
    {
        [Key]
        [Column(Order = 0)]
        public long SEQACESSO { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(15)]
        public string PROGRAMA { get; set; }

        [StringLength(200)]
        public string OBS { get; set; }
    }

}
