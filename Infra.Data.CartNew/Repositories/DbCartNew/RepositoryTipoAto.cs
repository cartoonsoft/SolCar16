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

        public IEnumerable<TipoAtoList> GetListTipoAtos(long? idTipoAtoPai)
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
                    Orientacao = T.Orientacao,
                    SiglaSeqAto = T.SiglaSeqAto

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
                    SiglaSeqAto = item.SiglaSeqAto,
                    ListaTipoAtosFihos = GetListTipoAtos(item.Id).ToList()
                });
            }

            return listaDtoTipoAtoList;
        }

        public IEnumerable<string> GetListEntidadesTipoAto(long? IdTipoAto, long IdCtaAcessoSist)
        {
            List<string> lista = new List<string>();

            var listaCampos =
                from ta in _contextRepository.DbTipoAto.Where(t => (t.Id == IdTipoAto) && (t.IdCtaAcessoSist == IdCtaAcessoSist))
                join tac in _contextRepository.DbTipoAtoCampo on ta.Id equals tac.IdTipoAto
                join ac in _contextRepository.DbCampoTipoAto on tac.IdCampoTipoAto equals ac.Id
                orderby ac.Entidade
                select new
                {
                    Entidade = ac.Entidade
                };

            foreach (var campo in listaCampos.Distinct())
            {
                lista.Add(campo.Entidade);
            }

            return lista;
        }

        public IEnumerable<CampoTipoAto> GetListCamposTipoAto(long? IdTipoAto, long IdCtaAcessoSist, string entidade = null)
        {
            List<CampoTipoAto> campoTipoAtos = new List<CampoTipoAto>();

            var listaCampos =
                from ta  in _contextRepository.DbTipoAto.Where(t => (t.Id == IdTipoAto) && (t.IdCtaAcessoSist == IdCtaAcessoSist))
                join tac in _contextRepository.DbTipoAtoCampo on ta.Id equals tac.IdTipoAto 
                join cta in _contextRepository.DbCampoTipoAto.Where(cta => ((entidade == null) || (cta.Entidade.ToLower() == entidade.ToLower()))) on tac.IdCampoTipoAto equals cta.Id
                orderby cta.Entidade, cta.NomeCampo
                select new
                {
                    Id = cta.Id,
                    IdCtaAcessoSist = cta.IdCtaAcessoSist,
                    NomeCampo = cta.NomeCampo,
                    PlaceHolder = cta.PlaceHolder,
                    Campo = cta.Campo,
                    Entidade = cta.Entidade
                };

            foreach (var campo in listaCampos)
            {
                campoTipoAtos.Add(
                    new CampoTipoAto
                    {
                        Id = campo.Id,
                        IdCtaAcessoSist = campo.IdCtaAcessoSist,
                        Campo = campo.Campo,
                        NomeCampo = campo.NomeCampo,
                        Entidade = campo.Entidade,
                        PlaceHolder = campo.PlaceHolder
                    }
                );
            }

            return campoTipoAtos;
        }

    }
}
