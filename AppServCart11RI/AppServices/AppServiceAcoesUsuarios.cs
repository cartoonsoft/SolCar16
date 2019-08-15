﻿using System;
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
                resposta.Message = "Permissão concedido ao usuário com sucesso!";
            }
            else
            {
                resposta.Message = "Já foi concedida esta permissão ao usuário!";
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

        public IEnumerable<DtoMenu> ListaMenuUsuario(UsuarioIdentity usr)
        {
            return this.DsFactoryCartNew.AcoesUsuariosDs.ListaMenuUsuario(usr).ToList();
        }

   }
}
