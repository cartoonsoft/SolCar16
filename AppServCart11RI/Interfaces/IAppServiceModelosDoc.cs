using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.CartNew.Entities;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using AppServ.Core.Interfaces;

namespace AppServices.Cartorio.Interfaces
{
    public interface IAppServiceModelosDoc : IAppServiceCartorio<DtoModeloDoc, ModeloDoc>
    {
        /// <summary>
        /// SAvalr um Modelo de doc
        /// </summary>
        /// <param name="dtoArq"></param>
        /// <param name="IdUsuario"></param>
        /// <returns></returns>
        long? NovoModelo(DtoModeloDoc dtoArq);

        void EditarModelo(DtoModeloDoc dtoArq);

        bool Desativar(long Id, string IdUsuario);

        IEnumerable<DtoModeloDocxList> GetListModelosDocx(long? IdTipoAto = null);
    }

}
