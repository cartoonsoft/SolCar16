using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppServices.Cartorio.Interfaces.Base;
using Dto.Cartorio.Entities.Cadastros;
using Dto.Cartorio.Entities.Diversos;
using Dto.Car16.Entities.Cadastros;
using Domain.CartNew.Entities;

namespace AppServices.Cartorio.Interfaces
{
    public interface IAppServiceArquivoModeloDocx: IAppServiceCartorio<DtoArquivoModeloDocxModel, ArquivoModeloDocx>
    {
        /// <summary>
        /// SAvalr um Modelo de doc
        /// </summary>
        /// <param name="dtoArq"></param>
        /// <param name="IdUsuario"></param>
        /// <returns></returns>
        long? NovoModelo(DtoArquivoModeloDocxModel dtoArq, string IdUsuario);

        void EditarModelo(DtoArquivoModeloDocxModel dtoArq, string IdUsuario);

        bool Desativar(long Id, string IdUsuario);

        IEnumerable<DtoArquivoModeloDocxList> ListarArquivoModeloDocx(long? IdTipoAto = null);
        IEnumerable<DtoArquivoModeloSimplificadoDocxList> ListarArquivoModeloSimplificado(long? IdTipoAto = null);

    }
}
