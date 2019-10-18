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
    public class RepositoryAcao : RepositoryBaseReadWrite<Acao>, IRepositoryAcao
    {
        private readonly ContextMainCartNew _contextRepository;

        public RepositoryAcao(ContextMainCartNew contextRepository) : base(contextRepository)
        {
            _contextRepository = contextRepository;
        }

        public IEnumerable<AcaoMenuList> GetListMenuUsuario(string IdUsuario)
        {
            List<AcaoMenuList> acaoMenuList = new List<AcaoMenuList>();

            var listaAcaoMenu =
                from M in this._contextRepository.DbMenu
                join A1 in this._contextRepository.DbAcao.Where(a1 => a1.SeqAcesso == null) on M.IdAcao equals A1.Id into _a
                from A1 in _a.DefaultIfEmpty()
                join UA in this._contextRepository.DbUsuarioAcao.Where(ua2 => ua2.IdUsuario == IdUsuario) on A1.Id equals UA.IdAcao into _ua
                from UA in _ua.DefaultIfEmpty()
                orderby (M.Ordem)
                select new 
                {
                    Id = M.Id,
                    IdCtaAcessoSist = M.IdCtaAcessoSist,
                    IdTipoMenu = M.IdTipoMenu,
                    IdMenuPai = M.IdMenuPai,
                    IdAcao = M.IdAcao,
                    Ordem = M.Ordem,
                    DescricaoMenu = M.DescricaoMenu ?? A1.DescricaoPequeno,
                    DescricaoMenuMobile = M.DescricaoMenuMobile ?? A1.DescricaoPequeno,
                    IconeWeb = M.IconeWeb ?? A1.IconeWeb ?? "fa-angle-double-right",
                    IconeMobile = M.IconeMobile ?? A1.IconeMobile ?? "fa-angle-double-right",
                    Action = A1.Action,
                    Controller = A1.Controller,
                    Parametros = A1.Parametros,
                    DescricaoBalao = A1.DescricaoBalao,
                    DescricaoTip = A1.DescricaoTip,
                    Orientacao = A1.Orientacao,
                    Permissao = UA.Equals(null) ? false : true,
                    Ativo = M.Ativo,
                    EmManutencao = (!A1.EmManutencao) ? M.EmManutencao : A1.EmManutencao
                };

            foreach (var acaoMenu in listaAcaoMenu)
            {
                acaoMenuList.Add(new AcaoMenuList
                {


                });
            }

            return acaoMenuList;
        }
    }
}
