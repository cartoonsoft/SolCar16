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
    public interface IAppServiceModelosDocx : IAppServiceCartorio<DtoModeloDocx, ModeloDocx>
    {
        /// <summary>
        /// SAvalr um Modelo de doc
        /// </summary>
        /// <param name="dtoArq"></param>
        /// <param name="IdUsuario"></param>
        /// <returns></returns>
        long? NovoModelo(DtoModeloDocx dtoArq, string IdUsuario);

        void EditarModelo(DtoModeloDocx dtoArq, string IdUsuario);

        bool Desativar(long Id, string IdUsuario);

        IEnumerable<DtoModeloDocxList> ListarModelosDocx(long? IdTipoAto = null);

        IEnumerable<DtoModeloDocxSimplificadoList> ListarModeloSimplificado(long? IdTipoAto = null);
    }

}
