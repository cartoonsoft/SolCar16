using Domain.CartNew.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartNew.Interfaces.Repositories
{
    public interface IRepositoryUsuarioIdentity
    {
        UsuarioIdentity ObterPorId(string id);
        IEnumerable<UsuarioIdentity> ObterTodos();
        void LockUsuario(string id);
        void UnLockUsuario(string id);

    }
}
