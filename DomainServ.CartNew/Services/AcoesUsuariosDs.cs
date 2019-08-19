using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.UnitOfWork;
using DomainServ.CartNew.Base;
using DomainServ.CartNew.Interfaces;
using Dto.CartNew.Entities.Cart_11RI.Diversos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServ.CartNew.Services
{
    public class AcoesUsuariosDs: DomainServiceCartNew<Acao>, IAcoesUsuariosDs
    {
        public AcoesUsuariosDs(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            //
        }

        public IEnumerable<DtoMenu> ListaMenuUsuario(UsuarioIdentity usr)
        {
            IEnumerable<DtoMenu> Menu = new List<DtoMenu>();

            Menu = 
                from M in this.UfwCartNew.Repositories.GenericRepository<Menu>().Get()
                join A1 in this.UfwCartNew.Repositories.GenericRepository<Acao>().Get().Where(a1 =>a1.SeqAcesso == null) on M.IdAcao equals A1.Id into _a
                from A1 in _a.DefaultIfEmpty()
                join UA in this.UfwCartNew.Repositories.GenericRepository<UsuarioAcao>().Get().Where(ua2 => ua2.IdUsuario == usr.Id) on A1.Id equals UA.IdAcao into _ua
                from UA in _ua.DefaultIfEmpty()
                orderby (M.Ordem)
                select new DtoMenu
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
                    Permissao = UA.Equals(null) ? false || (usr.UserName == "ronaldo") : true,
                    Ativo = M.Ativo,
                    EmManutencao = (!A1.EmManutencao) ? M.EmManutencao : A1.EmManutencao
                };

            //se não for  admin retirar menus 
            //if (usr.Claims.Find(c => (c.Type == "AdminUsers") &&(c.Value == "true")) == null)
            //{

            //}

            return Menu;
        }

    }
}
