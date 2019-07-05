using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppServices.Cartorio.Interfaces.Base;
using Domain.Cartorio.Entities.CartorioNew;
using Dto.Cartorio.Entities.Cadastros;
using Dto.Cartorio.Entities.Diversos;

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
        long? SalvarModelo(DtoArquivoModeloDocxModel dtoArq, string IdUsuario);

        void EditarModelo(DtoArquivoModeloDocxModel dtoArq, string IdUsuario);

        bool Desativar(long Id, string IdUsuario);

        IEnumerable<DtoArquivoModeloDocxList> ListarArquivoModeloDocx(long? IdTipoAto = null);
        IEnumerable<DtoArquivoModeloSimplificadoDocxList> ListarArquivoModeloSimplificado(long? IdTipoAto = null);



        

        /*
        DtoDadosImovel GetCamposModeloMatricula(long[] listIdsPessoas, long? IdTipoAto, long? IdPrenotacao, long? IdMatricula);

        IEnumerable<DtoCamposValor> GetCamposPrenotacao(long? IdTipoAto, long? IdPrenotacao, long? IdMatricula);

        IEnumerable<DtoCamposValor> GetCamposImovel(long? IdTipoAto, long? IdPrenotacao, long? IdMatricula);

        IEnumerable<CamposArquivoModeloDocx> GetListaCamposIdTipoAto(long? IdTipoAto);
        */
    }
}
