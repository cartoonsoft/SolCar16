using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Entities;
using Domain.Core.Interfaces.Repositories;

namespace Domain.CartNew.Interfaces.Repositories
{
    public interface IRepositoryModeloDocx : IRepositoryBaseReadWrite<ModeloDocx>
    {
        byte[] GetBytesArquivo(long idArquivo);
        //IEnumerable<ArquivoModeloDocxList> ListarArquivoModeloDocx(long? IdTipoAto = null);

        //IEnumerable<ArquivoModeloSimplificadoDocxList> ListarArquivoModeloSimplificadoDocx(long? IdTipoAto = null);

        IEnumerable<CamposModeloDocx> GetListaCamposIdTipoAto(long? IdTipoAto);
    }
}
