using Domain.Car16.Entities;
using Domain.Car16.Interfaces.Repositories;
using Domain.Core.Interfaces.Data;
using Infra.Data.Car16.Context;
using Infra.Data.Car16.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Car16.Repositories
{
    public class RepositoryArquivoModeloDocx : RepositoryBase<ArquivoModeloDocx>, IRepositoryArquivoModeloDocx
    {
        private readonly ContextMainCar16 _contexRep;

        public RepositoryArquivoModeloDocx(ContextMainCar16 contexRep) : base(contexRep)
        {
            _contexRep = contexRep;
        }
    }
}
