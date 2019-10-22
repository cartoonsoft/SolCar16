using AutoMapper;
using Domain.CartNew.Entities;
using Domain.CartNew.Entities.Diversos;
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

        public IEnumerable<DtoMenuAcaoList> GetListMenuUsuario(UsuarioIdentity usr, long IdCtaAcessoSist)
        {
            IEnumerable<DtoMenuAcaoList> listaMenu = new List<DtoMenuAcaoList>();

            if (usr != null)
            {
                var lista = this.UfwCartNew.Repositories.RepositoryAcao.GetListMenuUsuario(usr.Id, IdCtaAcessoSist).ToList();
                listaMenu = Mapper.Map<List<MenuAcaoList>, List<DtoMenuAcaoList>>(lista);
            }

            //se não for  admin retirar menus 
            //if (usr.Claims.Find(c => (c.Type == "AdminUsers") &&(c.Value == "true")) == null)
            //{

            //}

            return listaMenu;
        }

    }
}
