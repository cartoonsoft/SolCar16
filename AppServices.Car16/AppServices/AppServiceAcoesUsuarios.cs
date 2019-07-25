using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Cartorio.AppServices.Base;
using AppServices.Cartorio.Interfaces;
using Domain.Car16.Entities.Car16New;
using Domain.Cartorio.Interfaces.UnitOfWork;
using Domain.Car16.Entities.Car16;
using Dto.Car16.Entities.Cadastros;
using Dto.Car16.Entities.Diversos;

namespace AppServices.Cartorio.AppServices
{
    public class AppServiceAcoesUsuarios : AppServiceCartorioNew<DtoAcao, Acao>, IAppServiceAcoesUsuarios
    {
        public AppServiceAcoesUsuarios(IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartorioNew UfwCartNew) : base(UfwCart, UfwCartNew)
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
    }
}
