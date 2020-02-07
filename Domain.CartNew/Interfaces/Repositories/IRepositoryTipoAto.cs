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
    public interface IRepositoryTipoAto : IRepositoryBaseReadWrite<TipoAto>
    {
        IEnumerable<TipoAtoList> GetListTipoAtos(long? idTipoAtoPai);

        IEnumerable<string> GetListEntidadesTipoAto(long? IdTipoAto, long IdCtaAcessoSist);

        IEnumerable<CampoTipoAto> GetListCamposTipoAto(long? IdTipoAto, long IdCtaAcessoSist, string entidade = null);
    }
}
