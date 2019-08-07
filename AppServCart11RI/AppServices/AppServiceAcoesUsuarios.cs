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
            //IEnumerable<DtoMenu> lista = null;


            var Resultado =
            from M in this.UfwCartNew.Repositories.GenericRepository<Menu>().Get()
            join A in this.UfwCartNew.Repositories.GenericRepository<Acao>().Get() on M.IdAcao equals A.Id into _a
            from A in _a.DefaultIfEmpty()
            select new 
            {
                Id = M.Id,
                IdContaAcessoSistema = M.IdContaAcessoSistema, 
                IdTipoMenu = M.IdTipoMenu,
                IdMenuPai =  M.IdMenuPai, 
                IdAcao = M.IdAcao, 
                Ordem = M.Ordem,
                DescricaoMenu = M.DescricaoMenu,
                DescricaoMenuMobile = M.DescricaoMenuMobile,
                IconeWeb = M.IconeWeb,
                IconeMobile = M.IconeMobile,
                Ativo = M.Ativo,
                EmManutencao = M.EmManutencao
            };

            //todo: ronaldo fazer ListaMenuUsusurio
            return null; //Resultado.ToList();
        }


   }
}
