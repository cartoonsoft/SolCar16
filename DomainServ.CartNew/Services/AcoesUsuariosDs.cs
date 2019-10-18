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

        public IEnumerable<DtoAcaoMenuList> GetListMenuUsuario(UsuarioIdentity usr)
        {
            IEnumerable<DtoAcaoMenuList> Menu = new List<DtoAcaoMenuList>();
            if (usr != null)
            {
                var lista = this.UfwCartNew.Repositories.RepositoryAcao.GetListMenuUsuario(usr.Id);

            }


            //se não for  admin retirar menus 
            //if (usr.Claims.Find(c => (c.Type == "AdminUsers") &&(c.Value == "true")) == null)
            //{

            //}

            return Menu;
        }

    }
}
