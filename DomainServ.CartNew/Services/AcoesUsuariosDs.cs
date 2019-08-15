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
                join A in this.UfwCartNew.Repositories.GenericRepository<Acao>().Get() on M.IdAcao equals A.Id into _a
                from A in _a.DefaultIfEmpty()
                join UA in this.UfwCartNew.Repositories.GenericRepository<UsuarioAcao>().Get().Where(ua2 => ua2.IdUsuario == usr.Id) on A.Id equals UA.IdAcao into _ua
                from UA in _ua.DefaultIfEmpty()
                orderby (M.Ordem)
                select new DtoMenu
                {
                    Id = M.Id,
                    IdContaAcessoSistema = M.IdContaAcessoSistema,
                    IdTipoMenu = M.IdTipoMenu,
                    IdMenuPai = M.IdMenuPai,
                    IdAcao = M.IdAcao,
                    Ordem = M.Ordem,
                    DescricaoMenu = M.DescricaoMenu ?? A.DescricaoPequeno,
                    DescricaoMenuMobile = M.DescricaoMenuMobile ?? A.DescricaoPequeno,
                    IconeWeb = M.IconeWeb ?? A.IconeWeb ?? "fa-angle-double-right",
                    IconeMobile = M.IconeMobile ?? A.IconeMobile ?? "fa-angle-double-right",
                    Action = A.Action,
                    Controller = A.Controller,
                    Parametros = A.Parametros,
                    DescricaoBalao = A.DescricaoBalao,
                    DescricaoTip = A.DescricaoTip,
                    Orientacao = A.Orientacao,
                    Permissao = UA.Equals(null) ? false : true,
                    Ativo = M.Ativo,
                    EmManutencao = (!A.EmManutencao) ? M.EmManutencao : A.EmManutencao
                };

            //se não for  admin retirar menus 
            //if (usr.Claims.Find(c => (c.Type == "AdminUsers") &&(c.Value == "true")) == null)
            //{

            //}


            return Menu;
        }

    }
}
