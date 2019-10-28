using Dto.CartNew.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI.Diversos
{
    public class DtoReservaImovel: DtoExecProc
    {
        public DtoReservaImovel()
        {
            //
            this.Imovel = null;
        }

        public string IdUsuario { get; set; }
        public DtoDadosImovel Imovel { get; set; }
    }
}
