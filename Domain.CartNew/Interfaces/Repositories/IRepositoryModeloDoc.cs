using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Entities;
using Domain.CartNew.Entities.Diversos;
using Domain.Core.Interfaces.Repositories;

namespace Domain.CartNew.Interfaces.Repositories
{
    public interface IRepositoryModeloDoc : IRepositoryBaseReadWrite<ModeloDoc>
    {
        byte[] GetBytesArquivo(long idArquivo);

        IEnumerable<ModeloDocxSimplificadoList> GetListModeloSimplificadoDocx(long? IdTipoAto = null);

        IEnumerable<CampoTipoAto> GetListCamposIdTipoAto(long? IdTipoAto, long IdCtaAcessoSist);

        IEnumerable<ModeloDocxList> GetListModelosDocx(long? IdTipoAto = null);

        IEnumerable<ModeloDocxList> GetListModelosDocx2(long? IdTipoAto = null);
    }
}
