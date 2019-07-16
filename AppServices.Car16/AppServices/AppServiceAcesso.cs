using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Cartorio.AppServices.Base;
using AppServices.Cartorio.Interfaces;
using Domain.Cartorio.Entities.CartorioNew;
using Domain.Cartorio.Interfaces.UnitOfWork;
using Dto.Cartorio.Entities.Cadastros;
using Dto.Car16.Entities.Cadastros;
using Domain.Cartorio.Entities.Cartorio;
using Dto.Car16.Entities.Diversos;
using Domain.Car16.Entities.Car16New;

namespace AppServices.Cartorio.AppServices
{
    public class AppServiceAcesso : AppServiceCartorioNew<DtoAcesso, ACESSO>, IAppServiceAcesso
    {
        public AppServiceAcesso(IUnitOfWorkDataBaseCartorio UfwCart, IUnitOfWorkDataBaseCartorioNew UfwCartNew) : base(UfwCart, UfwCartNew)
        {
            //
        }

        public DtoExcuteService AddUsrAcesso(long IdAcesso, string IdUsuario)
        {
            DtoExcuteService resposta = new DtoExcuteService();
            var usr = this.UfwCartNew.Repositories.GenericRepository<UsuarioAcesso>().GetWhere(u => (u.SeqAcesso == IdAcesso) && (u.IdUsuario == IdUsuario) && (u.IdContaAcessoSistema == 1)).FirstOrDefault();

            if ((usr == null) || (string.IsNullOrEmpty(usr.IdUsuario)))
            {
                UsuarioAcesso usrAcesso = new UsuarioAcesso();
                usrAcesso.IdUsuario = IdUsuario;
                usrAcesso.SeqAcesso = IdAcesso;
                usrAcesso.IdContaAcessoSistema = 1;

                this.UfwCartNew.Repositories.GenericRepository<UsuarioAcesso>().Add(usrAcesso);
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

        public DtoExcuteService RemoveUsrAcesso(long IdAcesso, string IdUsuario)
        {
            DtoExcuteService resposta = new DtoExcuteService();
            var usr = this.UfwCartNew.Repositories.GenericRepository<UsuarioAcesso>().GetWhere(u => (u.SeqAcesso == IdAcesso) && (u.IdUsuario == IdUsuario) && (u.IdContaAcessoSistema == 1)).FirstOrDefault();

            if ((usr != null) && (!string.IsNullOrEmpty(usr.IdUsuario)))
            {
                this.UfwCartNew.Repositories.GenericRepository<UsuarioAcesso>().Remove(usr);
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
