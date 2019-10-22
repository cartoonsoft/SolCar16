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

        public DtoExcuteService AddUsrAcao(long IdAcao, string IdUsuario)
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
                resposta.Message = "Permissão concedido ao usuário com sucesso!";
            }
            else
            {
                resposta.Message = "Já foi concedida esta permissão ao usuário!";
            }
            
            return resposta;
        }

        public DtoExcuteService RemoveUsrAcao(long IdAcao, string IdUsuario)
        {
            DtoExcuteService resposta = new DtoExcuteService();
            var usr = this.UfwCartNew.Repositories.GenericRepository<UsuarioAcao>().GetWhere(u => (u.IdUsuario == IdUsuario) && (u.IdAcao == IdAcao)).FirstOrDefault();

            if ((usr != null) && (!string.IsNullOrEmpty(usr.IdUsuario)))
            {
                this.UfwCartNew.Repositories.GenericRepository<UsuarioAcao>().Remove(usr);
                this.UfwCartNew.SaveChanges();
                resposta.Execute = true;
                resposta.Message = "Permissão removida do usuário com sucesso!";
            }
            else
            {
                resposta.Message = "Usuário não tem esta Permissão!";
            }

            return resposta;
        }

        public IEnumerable<DtoMenuAcaoList> GetListMenuUsuario(UsuarioIdentity usr, long IdCtaAcessoSist)
        {
            return this.DsFactoryCartNew.AcoesUsuariosDs.GetListMenuUsuario(usr, IdCtaAcessoSist).ToList();
        }

   }
}
