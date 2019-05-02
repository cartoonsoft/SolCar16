using Domain.Car16.Entities.Car16;
using Domain.Car16.Interfaces.Repositories;
using Domain.Core.Interfaces.Data;
using Infra.Data.Car16.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Car16.Repositories
{
    public class RepositoryPESXPRE : RepositoryBaseRead<PESXPRE>, IRepositoryPESXPRE
    {
        public RepositoryPESXPRE(IContextCore context) : base(context)
        {
        }
    }
}
