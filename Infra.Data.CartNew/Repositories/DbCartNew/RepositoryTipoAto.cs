using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Entities;
using Domain.CartNew.Entities.Diversos;
using Domain.CartNew.Interfaces.Repositories;
using Infra.Data.CartNew.Context;
using Infra.Data.Core.Repositories;

namespace Infra.Data.CartNew.Repositories.DbCartNew
{
    public class RepositoryTipoAto : RepositoryBaseReadWrite<TipoAto>, IRepositoryTipoAto
    {
        private readonly ContextMainCartNew _contextRepository;

        public RepositoryTipoAto(ContextMainCartNew contextRepository) : base(contextRepository)
        {
            _contextRepository = contextRepository;
        }

        public IEnumerable<TipoAtoList> ListaTipoAtos(long? idTipoAtoPai)
        {
            List<TipoAtoList> listaDtoTipoAtoList = new List<TipoAtoList>();
            
            var lista =
                from T in _contextRepository.DbTipoAto.Where(ta => (ta.IdTipoAtoPai == idTipoAtoPai))
                orderby T.Descricao
                select new TipoAtoList
                {
                    Id = T.Id,
                    IdCtaAcessoSist = T.IdCtaAcessoSist,
                    IdTipoAtoPai = T.IdTipoAtoPai,
                    Descricao = T.Descricao,
                    Orientacao = T.Orientacao
                };

            foreach (var item in lista)
            {
                listaDtoTipoAtoList.Add(new TipoAtoList()
                {
                    Id = item.Id,
                    IdCtaAcessoSist = item.IdCtaAcessoSist,
                    IdTipoAtoPai = item.IdTipoAtoPai,
                    Descricao = item.Descricao,
                    Orientacao = item.Orientacao,
                    ListaTipoAtosFihos = ListaTipoAtos(item.Id).ToList()
                });
            }

            return listaDtoTipoAtoList;
        }
    }
}
