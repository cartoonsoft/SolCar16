using Domain.CartNew.Entities;
using Domain.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartNew.Interfaces.Repositories
{
    public interface IRepositoryUsuarioIdentity: IRepositoryBaseReadWrite<UsuarioIdentity>
    {
        void LockUsuario(string id);
        void UnLockUsuario(string id);
    }
}
