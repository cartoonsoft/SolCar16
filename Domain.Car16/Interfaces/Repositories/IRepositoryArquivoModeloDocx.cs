using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities.Car16New;
using Domain.Cartorio.Entities.Diversas;
using Domain.Core.Interfaces.Repositories;

namespace Domain.Cartorio.Interfaces.Repositories
{
    public interface IRepositoryArquivoModeloDocx : IRepositoryBaseReadWrite<ArquivoModeloDocx>
    {
        byte[] GetBytesArquivo(long idArquivo);
        IEnumerable<ArquivoModeloDocxList> ListarArquivoModeloDocx(long? IdTipoAto = null);

        IEnumerable<ArquivoModeloSimplificadoDocxList> ListarArquivoModeloSimplificadoDocx(long? IdTipoAto = null);

        IEnumerable<CamposArquivoModeloDocx> GetListaCamposIdTipoAto(long? IdTipoAto);
    }
}
