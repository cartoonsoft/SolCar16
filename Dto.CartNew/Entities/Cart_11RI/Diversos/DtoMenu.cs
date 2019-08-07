using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI.Diversos
{
    public class DtoMenu
    {
        [Key]
        public long? Id { get; set; }

        public long IdContaAcessoSistema { get; set; }

        public long IdTipoMenu { get; set; }

        public long IdMenuPai { get; set; }

        public long IdAcao { get; set; }

        public int Ordem { get; set; }

        public string DescricaoMenu { get; set; }

        public string DescricaoMenuMobile { get; set; }

        public string IconeWeb { get; set; }

        public string IconeMobile { get; set; }

        public bool Ativo { get; set; }

        public bool EmManutencao { get; set; }
    }
}
