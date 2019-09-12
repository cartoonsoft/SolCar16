using Dto.CartNew.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI
{
    public class DtoTipoAto: DtoEntityBaseModel
    {
        [Key]
        public override long? Id { get; set; }

        public long IdCtaAcessoSist { get; set; }

        public long? IdTipoAtoPai { get; set; }

        public string Descricao { get; set; }

        public string Orientacao { get; set; }
    }
}
