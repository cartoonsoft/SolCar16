using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.Repositories;
using Infra.Data.CartNew.Context;
using Infra.Data.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.CartNew.Repositories.Identity
{
    public class RepositoryUsuarioIdentity : RepositoryBaseReadWrite<UsuarioIdentity>,  IRepositoryUsuarioIdentity
    {
        private readonly ContextMainCartNew _contextRepository;

        public RepositoryUsuarioIdentity(ContextMainCartNew contextRepository) : base(contextRepository)
        {
            _contextRepository = contextRepository;
        }

        public void LockUsuario(string id)
        {
            if (_contextRepository.DbUsuariosIdentity.Find(id) != null)
            {
                _contextRepository.DbUsuariosIdentity.Find(id).LockoutEnabled = true;
                _contextRepository.SaveChanges();
            }
        }

        public void UnLockUsuario(string id)
        {
            if (_contextRepository.DbUsuariosIdentity.Find(id) != null)
            {
                _contextRepository.DbUsuariosIdentity.Find(id).LockoutEnabled = false;
                _contextRepository.SaveChanges();
            }
        }
    }
}
