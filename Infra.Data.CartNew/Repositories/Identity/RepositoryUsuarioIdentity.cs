using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.Repositories;
using Infra.Data.CartNew.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.CartNew.Repositories.Identity
{
    public class RepositoryUsuarioIdentity : IRepositoryUsuarioIdentity
    {
        private readonly ContextMainCartNew _db;
        public RepositoryUsuarioIdentity()
        {
            //_db = new ContextMainCartNew()
        }

        public UsuarioIdentity ObterPorId(string id)
        {
            return _db.DbUsuariosIdentity.Find(id);
        }

        public IEnumerable<UsuarioIdentity> ObterTodos()
        {
            return _db.DbUsuariosIdentity.ToList();
        }

        public void LockUsuario(string id)
        {
            if (_db.DbUsuariosIdentity.Find(id) != null)
            {
                _db.DbUsuariosIdentity.Find(id).LockoutEnabled = true;
                _db.SaveChanges();
            }
        }

        public void UnLockUsuario(string id)
        {
            if (_db.DbUsuariosIdentity.Find(id) != null)
            {
                _db.DbUsuariosIdentity.Find(id).LockoutEnabled = false;
                _db.SaveChanges();
            }
        }
    }
}
