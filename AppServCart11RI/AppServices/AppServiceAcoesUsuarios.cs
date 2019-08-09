using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServ.Core.AppServices;
using AppServices.Cartorio.Interfaces;
using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.UnitOfWork;
using Dto.CartNew.Entities.Cart_11RI;
using Dto.CartNew.Entities.Cart_11RI.Diversos;

namespace AppServCart11RI.AppServices
{
    public class AppServiceAcoesUsuarios : AppServiceCartorio<DtoAcao, Acao>, IAppServiceAcoesUsuarios
    {
        public AppServiceAcoesUsuarios(IUnitOfWorkDataBaseCartNew UfwCartNew) : base(UfwCartNew)
        {
            //
        }

        public DtoExcuteService AddUsrAcesso(long IdAcao, string IdUsuario)
        {
            DtoExcuteService resposta = new DtoExcuteService();
            var usr = this.UfwCartNew.Repositories.GenericRepository<UsuarioAcao>().GetWhere(u => (u.IdUsuario == IdUsuario) && (u.IdAcao ==  IdAcao)).FirstOrDefault();

            if ((usr == null) || (string.IsNullOrEmpty(usr.IdUsuario)))
            {
                UsuarioAcao usrAcao = new UsuarioAcao();
                usrAcao.IdUsuario = IdUsuario;
                usrAcao.IdAcao = IdAcao;

                this.UfwCartNew.Repositories.GenericRepository<UsuarioAcao>().Add(usrAcao);
                this.UfwCartNew.SaveChanges();
                resposta.Execute = true;
                resposta.Message = "Acesso concedido ao usuário com sucesso!";
            }
            else
            {
                resposta.Message = "Já foi concedida este acesso aoa usuário!";
            }
            
            return resposta;
        }

        public DtoExcuteService RemoveUsrAcesso(long IdAcao, string IdUsuario)
        {
            DtoExcuteService resposta = new DtoExcuteService();
            var usr = this.UfwCartNew.Repositories.GenericRepository<UsuarioAcao>().GetWhere(u => (u.IdUsuario == IdUsuario) && (u.IdAcao == IdAcao)).FirstOrDefault();

            if ((usr != null) && (!string.IsNullOrEmpty(usr.IdUsuario)))
            {
                this.UfwCartNew.Repositories.GenericRepository<UsuarioAcao>().Remove(usr);
                this.UfwCartNew.SaveChanges();
                resposta.Execute = true;
                resposta.Message = "Acesso removido do usuário com sucesso!";
            }
            else
            {
                resposta.Message = "Usuário não tem este acesso!";
            }

            return resposta;
        }

        public IEnumerable<DtoMenu> ListaMenuUsusurio (string IdUsuario)
        {
            var Resultado =
            from M in this.UfwCartNew.Repositories.GenericRepository<Menu>().Get()
            join A in this.UfwCartNew.Repositories.GenericRepository<Acao>().Get() on M.IdAcao equals A.Id into _a
            from A in _a.DefaultIfEmpty()
            join UA in this.UfwCartNew.Repositories.GenericRepository<UsuarioAcao>().Get().Where(ua2 => ua2.IdUsuario == IdUsuario) on A.Id equals UA.IdAcao into _ua
            from UA in _ua.DefaultIfEmpty()
            orderby (M.Ordem)
            select new DtoMenu
            {
                Id = M.Id,
                IdContaAcessoSistema = M.IdContaAcessoSistema, 
                IdTipoMenu = M.IdTipoMenu,
                IdMenuPai =  M.IdMenuPai, 
                IdAcao = M.IdAcao, 
                Ordem = M.Ordem,
                DescricaoMenu = M.DescricaoMenu?? A.DescricaoPequeno,
                DescricaoMenuMobile = M.DescricaoMenuMobile?? A.DescricaoPequeno,
                IconeWeb = M.IconeWeb?? A.IconeWeb?? "fa-angle-double-right",
                IconeMobile = M.IconeMobile?? A.IconeMobile?? "fa-angle-double-right",
                Action = A.Action,
                Controller = A.Controller,
                Parametros = A.Parametros,
                DescricaoBalao = A.DescricaoBalao,
                DescricaoTip = A.DescricaoTip,
                Orientacao = A.Orientacao,
                Permissao = UA.Equals(null)?false:true,
                Ativo = M.Ativo,
                EmManutencao = (!A.EmManutencao)?M.EmManutencao: A.EmManutencao
            };

            //todo: ronaldo fazer ListaMenuUsusurio
            return Resultado.ToList(); //Resultado.ToList();
        }

   }
}
